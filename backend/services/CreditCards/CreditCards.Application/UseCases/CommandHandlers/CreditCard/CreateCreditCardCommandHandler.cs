using AutoMapper;
using ch4rniauski.BankApp.CreditCards.Application.Common.Errors;
using ch4rniauski.BankApp.CreditCards.Application.Common.Results;
using ch4rniauski.BankApp.CreditCards.Application.Contracts.HashProviders;
using ch4rniauski.BankApp.CreditCards.Application.Contracts.Repositories;
using ch4rniauski.BankApp.CreditCards.Application.Contracts.SensitiveDataProviders;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCard;
using ch4rniauski.BankApp.CreditCards.Application.UseCases.Commands.CreditCard;
using ch4rniauski.BankApp.CreditCards.Domain.Entities;
using ch4rniauski.BankApp.CreditCards.Grpc;
using Grpc.Net.Client;
using MediatR;

namespace ch4rniauski.BankApp.CreditCards.Application.UseCases.CommandHandlers.CreditCard;

public sealed class CreateCreditCardCommandHandler : IRequestHandler<CreateCreditCardCommand, Result<CreateCreditCardResponseDto>>
{
    private readonly ICreditCardRepository _creditCardRepository;
    private readonly IMapper _mapper;
    private readonly ICvvProvider _cvvProvider;
    private readonly IPinCodeProvider _pinCodeProvider;
    private readonly IHashProvider _hashProvider;
    
    private const string AuthenticationServiceAddress = "http://authentication-api:8080";

    public CreateCreditCardCommandHandler(
        ICreditCardRepository creditCardRepository,
        IMapper mapper,
        ICvvProvider cvvProvider,
        IPinCodeProvider pinCodeProvider,
        IHashProvider hashProvider)
    {
        _creditCardRepository = creditCardRepository;
        _mapper = mapper;
        _cvvProvider = cvvProvider;
        _pinCodeProvider = pinCodeProvider;
        _hashProvider = hashProvider;
    }

    public async Task<Result<CreateCreditCardResponseDto>> Handle(CreateCreditCardCommand request, CancellationToken cancellationToken)
    {
        using var channel = GrpcChannel.ForAddress(AuthenticationServiceAddress);
        var client = new ClientsService.ClientsServiceClient(channel);

        var grpcRequest = new CheckIfClientExistsRequest
        {
            ClientId = request.Request.CardHolderId.ToString()
        };
        
        var response = await client.CheckIfClientExistsAsync(
            request: grpcRequest,
            cancellationToken: cancellationToken);

        if (!response.DoesExist)
        {
            return Result<CreateCreditCardResponseDto>.Failure(
                Error.NotFound(
                    $"Client with id {request.Request.CardHolderId} does not exist"
                    ));
        }
        
        var creditCard = _mapper.Map<CreditCardEntity>(request.Request);

        var cvv = _cvvProvider.GenerateCvvCode();
        var pinCode = _pinCodeProvider.GeneratePinCode();

        var cvvHash = _hashProvider.GenerateHash(cvv);
        var pinCodeHash = _hashProvider.GenerateHash(pinCode);
        
        creditCard.CvvHash = cvvHash;
        creditCard.PinCodeHash = pinCodeHash;
        
        var isCreated = await _creditCardRepository.CreateAsync(creditCard, cancellationToken);

        if (!isCreated)
        {
            return Result<CreateCreditCardResponseDto>.Failure(
                Error.InternalError(
                    "Error was occured while creating Credit Card"
                    ));
        }
        
        var responseDto = _mapper.Map<CreateCreditCardResponseDto>(creditCard);
        
        return Result<CreateCreditCardResponseDto>.Success(responseDto);
    }
}
