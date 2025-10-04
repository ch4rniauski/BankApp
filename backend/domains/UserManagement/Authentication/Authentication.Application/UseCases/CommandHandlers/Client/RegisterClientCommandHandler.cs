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
using Microsoft.AspNetCore.Identity;

namespace ch4rniauski.BankApp.Authentication.Application.UseCases.CommandHandlers.Client;

public sealed class RegisterClientCommandHandler : IRequestHandler<RegisterClientCommand, Result<RegisterClientResponseDto>>
{
    private readonly IClientRepository _clientRepository;
    private readonly IValidator<RegisterClientRequestDto> _validator;
    private readonly IMapper _mapper;

    public RegisterClientCommandHandler(
        IClientRepository clientRepository,
        IValidator<RegisterClientRequestDto> validator,
        IMapper mapper)
    {
        _clientRepository = clientRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<Result<RegisterClientResponseDto>> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var message = string.Join("", validationResult.Errors);
            
            return Result<RegisterClientResponseDto>
                .Failure(Error.FailedValidation(message));
        }
        
        var doesExist = await _clientRepository.GetByEmailAsync(request.Request.Email, cancellationToken);

        if (doesExist is not null)
        {
            return Result<RegisterClientResponseDto>
                .Failure(Error.AlreadyExists(
                    $"Client with email {request.Request.Email} already exists"
                    ));
        }
        
        var client = _mapper.Map<ClientEntity>(request.Request);
        
        client.PasswordHash = new PasswordHasher<ClientEntity>().HashPassword(client, request.Request.Password);
        
        var isRegistered = await _clientRepository.AddAsync(client, cancellationToken);

        if (!isRegistered)
        {
            return Result<RegisterClientResponseDto>
                .Failure(Error.InternalError(
                    $"Client with email {request.Request.Email} was not registered"
                    ));
        }
        
        var response = _mapper.Map<RegisterClientResponseDto>(client);
        
        return Result<RegisterClientResponseDto>.Success(response);
    }
}
