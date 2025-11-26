using ch4rniauski.BankApp.Authentication.Application.Common.Errors;
using ch4rniauski.BankApp.Authentication.Application.Common.Results;
using ch4rniauski.BankApp.Authentication.Application.Contracts.Jwt;
using ch4rniauski.BankApp.Authentication.Application.Contracts.Repositories;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Requests;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Responses;
using ch4rniauski.BankApp.Authentication.Application.UseCases.CommandHandlers.Client;
using ch4rniauski.BankApp.Authentication.Application.UseCases.Commands.Client;
using ch4rniauski.BankApp.Authentication.Domain.Entities;
using ch4rniauski.BankApp.Authentication.Tests.Helpers.DataProviders;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace ch4rniauski.BankApp.Authentication.Tests.Tests.UnitTests.Clients;

public sealed class LoginClientUnitTests
{
    private readonly LoginClientCommandHandler _commandHandler;
    private readonly Mock<IClientRepository> _clientRepositoryMock = new();
    private readonly Mock<ITokenProvider> _tokenProviderMock = new();
    private readonly Mock<IValidator<LoginClientRequestDto>> _validatorMock = new();
    
    public LoginClientUnitTests()
    {
        _commandHandler = new LoginClientCommandHandler(
            _clientRepositoryMock.Object,
            _tokenProviderMock.Object,
            _validatorMock.Object);
    }

    [Fact]
    private async Task LoginClient_ReturnsSuccessfulResult()
    {
        // Arrange
        const bool isSuccess = true;
        
        var requestDto = ClientDataProvider.GenerateLoginClientRequestDto();
        var command = new LoginClientCommand(requestDto);
        var client = new ClientEntity();
        
        var passwordHash = new PasswordHasher<ClientEntity>().HashPassword(client, requestDto.Password);
        
        client.PasswordHash = passwordHash;

        _validatorMock.Setup(v => v.ValidateAsync(
                requestDto,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        
        _clientRepositoryMock.Setup(r => r.GetByEmailAsync(
                requestDto.Email,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(client);

        _clientRepositoryMock.Setup(r => r.UpdateAsync(
                client,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.IsType<Result<LoginClientResponseDto>>(response);
        Assert.IsType<LoginClientResponseDto>(response.Value);
        Assert.NotNull(response.Value);
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.Null(response.Error);
    }
    
    [Fact]
    private async Task LoginClient_ReturnsFailedResult_WithValidationError()
    {
        // Arrange
        const bool isSuccess = false;
        
        var requestDto = ClientDataProvider.GenerateLoginClientRequestDto();
        var command = new LoginClientCommand(requestDto);

        _validatorMock.Setup(v => v.ValidateAsync(
                requestDto,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult
            {
                Errors =
                [
                    new ValidationFailure()
                ]
            });

        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.IsType<Result<LoginClientResponseDto>>(response);
        Assert.IsType<ValidationError>(response.Error);
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.NotNull(response.Error);
        Assert.Null(response.Value);
    }
    
    [Fact]
    private async Task LoginClient_ReturnsFailedResult_NotFoundError()
    {
        // Arrange
        const bool isSuccess = false;
        
        var requestDto = ClientDataProvider.GenerateLoginClientRequestDto();
        var command = new LoginClientCommand(requestDto);
        ClientEntity? client = null;

        _validatorMock.Setup(v => v.ValidateAsync(
                requestDto,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        
        _clientRepositoryMock.Setup(r => r.GetByEmailAsync(
                requestDto.Email,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(client);

        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.IsType<Result<LoginClientResponseDto>>(response);
        Assert.IsType<NotFoundError>(response.Error);
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.NotNull(response.Error);
        Assert.Null(response.Value);
    }
    
    [Fact]
    private async Task LoginClient_ReturnsFailedResult_UnauthorizedError()
    {
        // Arrange
        const bool isSuccess = false;
        const string incorrectPasswordHash = "password";
        
        var requestDto = ClientDataProvider.GenerateLoginClientRequestDto();
        var command = new LoginClientCommand(requestDto);
        
        var client = new ClientEntity
        {
            PasswordHash = incorrectPasswordHash,
        };

        _validatorMock.Setup(v => v.ValidateAsync(
                requestDto,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        
        _clientRepositoryMock.Setup(r => r.GetByEmailAsync(
                requestDto.Email,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(client);

        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.IsType<Result<LoginClientResponseDto>>(response);
        Assert.IsType<UnauthorizedError>(response.Error);
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.NotNull(response.Error);
        Assert.Null(response.Value);
    }
    
    [Fact]
    private async Task LoginClient_ReturnsFailedResult_InternalServerErrorError()
    {
        // Arrange
        const bool isSuccess = false;
        
        var requestDto = ClientDataProvider.GenerateLoginClientRequestDto();
        var command = new LoginClientCommand(requestDto);
        var client = new ClientEntity();
        
        var passwordHash = new PasswordHasher<ClientEntity>().HashPassword(client, requestDto.Password);
        
        client.PasswordHash = passwordHash;

        _validatorMock.Setup(v => v.ValidateAsync(
                requestDto,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        
        _clientRepositoryMock.Setup(r => r.GetByEmailAsync(
                requestDto.Email,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(client);
        
        _clientRepositoryMock.Setup(r => r.UpdateAsync(
                client,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.IsType<Result<LoginClientResponseDto>>(response);
        Assert.IsType<InternalServerError>(response.Error);
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.NotNull(response.Error);
        Assert.Null(response.Value);
    }
}
