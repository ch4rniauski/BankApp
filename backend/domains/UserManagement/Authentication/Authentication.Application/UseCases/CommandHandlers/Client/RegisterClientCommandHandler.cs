using AutoMapper;
using ch4rniauski.BankApp.Authentication.Application.Contracts.Repositories;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Responses;
using ch4rniauski.BankApp.Authentication.Application.UseCases.Commands.Client;
using ch4rniauski.BankApp.Authentication.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ch4rniauski.BankApp.Authentication.Application.UseCases.CommandHandlers.Client;

public sealed class RegisterClientCommandHandler : IRequestHandler<RegisterClientCommand, RegisterClientResponseDto>
{
    private readonly IClientRepository _clientRepository;
    private readonly IValidator<RegisterClientCommand> _validator;
    private readonly IMapper _mapper;

    public RegisterClientCommandHandler(
        IClientRepository clientRepository,
        IValidator<RegisterClientCommand> validator,
        IMapper mapper)
    {
        _clientRepository = clientRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<RegisterClientResponseDto> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var message = string.Join("", validationResult.Errors);
            
            throw new ValidationException(message);
        }
        
        var doesExist = await _clientRepository.GetByEmailAsync(request.Request.Email, cancellationToken);

        if (doesExist is not null)
        {
            throw new Exception("The client is already registered.");
        }
        
        var client = _mapper.Map<ClientEntity>(request.Request);
        
        client.PasswordHash = new PasswordHasher<ClientEntity>().HashPassword(client, request.Request.Password);
        
        var isRegistered = await _clientRepository.AddAsync(client, cancellationToken);

        if (!isRegistered)
        {
            throw new Exception("The client wasn't registered.");
        }
        
        return _mapper.Map<RegisterClientResponseDto>(client);
    }
}