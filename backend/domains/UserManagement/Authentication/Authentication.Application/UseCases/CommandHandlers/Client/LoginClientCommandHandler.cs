using ch4rniauski.BankApp.Authentication.Application.Contracts.Jwt;
using ch4rniauski.BankApp.Authentication.Application.Contracts.Repositories;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Requests;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Responses;
using ch4rniauski.BankApp.Authentication.Application.UseCases.Commands.Client;
using ch4rniauski.BankApp.Authentication.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ch4rniauski.BankApp.Authentication.Application.UseCases.CommandHandlers.Client;

public sealed class LoginClientCommandHandler : IRequestHandler<LoginClientCommand, LoginClientResponseDto>
{
    private readonly IClientRepository _clientRepository;
    private readonly ITokenProvider _tokenProvider;
    private readonly IValidator<LoginClientRequestDto> _validator;

    public LoginClientCommandHandler(
        IClientRepository clientRepository,
        ITokenProvider tokenProvider,
        IValidator<LoginClientRequestDto> validator)
    {
        _clientRepository = clientRepository;
        _tokenProvider = tokenProvider;
        _validator = validator;
    }

    public async Task<LoginClientResponseDto> Handle(LoginClientCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            var message = string.Join("", validationResult.Errors);
            
            throw new ValidationException(message);
        }
        
        var client = await _clientRepository.GetByEmailAsync(request.Request.Email, cancellationToken) 
                     ?? throw new Exception("Client not found");
        
        var isValidPassword = new PasswordHasher<ClientEntity>().VerifyHashedPassword(client, client.PasswordHash, request.Request.Password);

        if (isValidPassword != PasswordVerificationResult.Success)
        {
            throw new Exception("Invalid password");
        }
        
        var accessToken = _tokenProvider.GenerateAccessToken(client);
        var refreshToken = _tokenProvider.GenerateRefreshToken();
        
        client.RefreshToken = refreshToken;
        
        var isUpdated = await _clientRepository.UpdateAsync(client, cancellationToken);

        if (!isUpdated)
        {
            throw new Exception("An Exception was thrown while updating client refresh token");
        }
        
        return new LoginClientResponseDto(accessToken, refreshToken);
    }
}