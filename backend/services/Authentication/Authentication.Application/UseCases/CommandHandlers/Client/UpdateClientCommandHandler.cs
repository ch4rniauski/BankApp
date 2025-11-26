using AutoMapper;
using ch4rniauski.BankApp.Authentication.Application.Common.Errors;
using ch4rniauski.BankApp.Authentication.Application.Common.Results;
using ch4rniauski.BankApp.Authentication.Application.Contracts.Repositories;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Requests;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Responses;
using ch4rniauski.BankApp.Authentication.Application.UseCases.Commands.Client;
using ch4rniauski.BankApp.Authentication.Domain.Entities;
using FluentValidation;
using MediatR;

namespace ch4rniauski.BankApp.Authentication.Application.UseCases.CommandHandlers.Client;

internal sealed class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Result<UpdateClientResponseDto>>
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateClientRequestDto> _validator;

    public UpdateClientCommandHandler(
        IClientRepository clientRepository,
        IMapper mapper,
        IValidator<UpdateClientRequestDto> validator)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
        _validator = validator;
    }
    
    public async Task<Result<UpdateClientResponseDto>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            var message = string.Join("", validationResult.Errors);
            
            return Result<UpdateClientResponseDto>
                .Failure(Error.FailedValidation(message));
        }

        var doesExist = await _clientRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (doesExist is null)
        {
            return Result<UpdateClientResponseDto>
                .Failure(Error.NotFound(
                    $"Client with ID {request.Id} does not exist"
                ));
        }
        
        var updatedClient = _mapper.Map<ClientEntity>(request.Request);
        
        var isUpdated = await _clientRepository.UpdateAsync(updatedClient, cancellationToken);

        if (!isUpdated)
        {
            return Result<UpdateClientResponseDto>
                .Failure(Error.InternalError(
                    "Client's data was not updated"
                ));
        }
        
        var response = _mapper.Map<UpdateClientResponseDto>(updatedClient);
        
        return Result<UpdateClientResponseDto>.Success(response);
    }
}
