using AutoMapper;
using ch4rniauski.BankApp.CreditCards.Grpc;
using ch4rniauski.BankApp.MoneyTransfer.Application.Common.Errors;
using ch4rniauski.BankApp.MoneyTransfer.Application.Common.Results;
using ch4rniauski.BankApp.MoneyTransfer.Application.Contracts.Repositories;
using ch4rniauski.BankApp.MoneyTransfer.Application.DTO.Requests.Payments;
using ch4rniauski.BankApp.MoneyTransfer.Application.DTO.Responses.Payments;
using ch4rniauski.BankApp.MoneyTransfer.Application.UseCases.Commands.Payments;
using ch4rniauski.BankApp.MoneyTransfer.Domain.Entities;
using ch4rniauski.BankApp.MoneyTransfer.Grpc;
using FluentValidation;
using Grpc.Net.Client;
using MediatR;

namespace ch4rniauski.BankApp.MoneyTransfer.Application.UseCases.CommandHandlers.Payments;

internal sealed class TransferMoneyToCardCommandHandler : IRequestHandler<TransferMoneyToCardCommand, Result<TransferMoneyResponseDto>>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IValidator<TransferMoneyRequestDto> _validator;
    private readonly IMapper _mapper;
    
    private const string AuthenticationServiceAddress = "";
    private const string CreditCardServiceAddress = "";

    public TransferMoneyToCardCommandHandler(
        IPaymentRepository paymentRepository,
        IValidator<TransferMoneyRequestDto> validator,
        IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<Result<TransferMoneyResponseDto>> Handle(TransferMoneyToCardCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            var message = string.Join("", validationResult.Errors);
            
            return Result<TransferMoneyResponseDto>
                .Failure(Error.FailedValidation(message));
        }
        
        using (var channel = GrpcChannel.ForAddress(AuthenticationServiceAddress))
        {
            var client = new ClientsService.ClientsServiceClient(channel);
            var grpcRequest = new CheckIfClientsExistRequest
            {
                ClientIds =
                {
                    request.Request.SenderId.ToString(),
                    request.Request.ReceiverId.ToString()
                }
            };
            
            var grpcResponse = await client.CheckIfClientsExistAsync(
                request: grpcRequest,
                cancellationToken: cancellationToken);

            if (grpcResponse.DoExist.Any(b => b == false))
            {
                return Result<TransferMoneyResponseDto>
                    .Failure(Error.NotFound(
                        "At least one client was not found by the provided IDs"
                        ));
            }
        }

        TransferMoneyToCardResponse? creditCardGrpcResponse;
        using (var channel = GrpcChannel.ForAddress(CreditCardServiceAddress))
        {
            var client = new CreditCardsGrpc.CreditCardsGrpcClient(channel);

            var grpcRequest = _mapper.Map<TransferMoneyToCardRequest>(request.Request);

            creditCardGrpcResponse = await client.TransferMoneyToCardAsync(
                request: grpcRequest,
                cancellationToken: cancellationToken);

            if (creditCardGrpcResponse.HasErrorStatusCode)
            {
                return Result<TransferMoneyResponseDto>
                    .Failure(Error.FailedGrpcOperation(
                        creditCardGrpcResponse.ErrorStatusCode,
                        creditCardGrpcResponse.ErrorMessage));
            }
        }
        
        var payment = _mapper.Map<PaymentEntity>(creditCardGrpcResponse);
        _mapper.Map(request.Request, payment);
        
        var isCreated = await _paymentRepository.CreateAsync(payment, cancellationToken);

        if (!isCreated)
        {
            return Result<TransferMoneyResponseDto>.Failure(
                Error.InternalError(
                    "Error was occured while creating Payment"
                ));
        }
        
        var response = _mapper.Map<TransferMoneyResponseDto>(payment);
        
        return Result<TransferMoneyResponseDto>.Success(response);
    }
}
