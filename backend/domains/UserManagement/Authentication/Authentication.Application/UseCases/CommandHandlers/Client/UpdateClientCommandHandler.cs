using AutoMapper;
using ch4rniauski.BankApp.Authentication.Application.Contracts.Repositories;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Requests;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Responses;
using ch4rniauski.BankApp.Authentication.Application.UseCases.Commands.Client;
using ch4rniauski.BankApp.Authentication.Domain.Entities;
using FluentValidation;
using MediatR;

namespace ch4rniauski.BankApp.Authentication.Application.UseCases.CommandHandlers.Client;

public sealed class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, UpdateClientResponseDto>
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
    
    public async Task<UpdateClientResponseDto> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            var message = string.Join("", validationResult.Errors);
            
            throw new ValidationException(message);
        }
        
        var doesExist = await _clientRepository.GetByIdAsync(request.Id, cancellationToken)
                        ?? throw new Exception("Client not found");
        
        var updatedClient = _mapper.Map<ClientEntity>(request);
        
        var isUpdated = await _clientRepository.UpdateAsync(updatedClient, cancellationToken);

        if (!isUpdated)
        {
            throw new Exception("Client was not updated.");
        }
        
        return _mapper.Map<UpdateClientResponseDto>(updatedClient);
    }
}
