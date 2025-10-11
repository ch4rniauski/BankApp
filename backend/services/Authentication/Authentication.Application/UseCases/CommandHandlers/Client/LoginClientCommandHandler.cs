using ch4rniauski.BankApp.Authentication.Application.Common.Errors;
using ch4rniauski.BankApp.Authentication.Application.Common.Results;
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

public sealed class LoginClientCommandHandler : IRequestHandler<LoginClientCommand, Result<LoginClientResponseDto>>
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

    public async Task<Result<LoginClientResponseDto>> Handle(LoginClientCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            var message = string.Join("", validationResult.Errors);
            
            return Result<LoginClientResponseDto>
                .Failure(Error.FailedValidation(message));
        }
        
        var client = await _clientRepository.GetByEmailAsync(request.Request.Email, cancellationToken);
        
        if (client is null)
        {
            return Result<LoginClientResponseDto>
                .Failure(Error.NotFound(
                    $"Client with Email {request.Request.Email} does not exist"
                ));
        }
        
        var isValidPassword = new PasswordHasher<ClientEntity>()
            .VerifyHashedPassword(client, client.PasswordHash, request.Request.Password);

        if (isValidPassword != PasswordVerificationResult.Success)
        {
            return Result<LoginClientResponseDto>
                .Failure(Error.Unauthorized("Invalid password"));
        }
        
        var accessToken = _tokenProvider.GenerateAccessToken(client);
        var refreshToken = _tokenProvider.GenerateRefreshToken();
        
        client.RefreshToken = refreshToken;
        
        var isUpdated = await _clientRepository.UpdateAsync(client, cancellationToken);

        if (!isUpdated)
        {
            return Result<LoginClientResponseDto>
                .Failure(Error.InternalError(
                    "Exception was thrown while updating client refresh token"
                    ));
        }
        
        var response = new LoginClientResponseDto(accessToken, refreshToken);
        
        return Result<LoginClientResponseDto>.Success(response); 
    }
}
