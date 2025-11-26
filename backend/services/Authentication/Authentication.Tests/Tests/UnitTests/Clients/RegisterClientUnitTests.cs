using AutoMapper;
using ch4rniauski.BankApp.Authentication.Application.Common.Errors;
using ch4rniauski.BankApp.Authentication.Application.Common.Results;
using ch4rniauski.BankApp.Authentication.Application.Contracts.Repositories;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Requests;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Responses;
using ch4rniauski.BankApp.Authentication.Application.UseCases.CommandHandlers.Client;
using ch4rniauski.BankApp.Authentication.Application.UseCases.Commands.Client;
using ch4rniauski.BankApp.Authentication.Domain.Entities;
using ch4rniauski.BankApp.Authentication.Tests.Helpers.DataProviders;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace ch4rniauski.BankApp.Authentication.Tests.Tests.UnitTests.Clients;

public sealed class RegisterClientUnitTests
{
    private readonly RegisterClientCommandHandler _commandHandler;
    private readonly Mock<IClientRepository> _clientRepositoryMock = new();
    private readonly Mock<IValidator<RegisterClientRequestDto>> _validatorMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    public RegisterClientUnitTests()
    {
        _commandHandler = new RegisterClientCommandHandler(
            _clientRepositoryMock.Object,
            _validatorMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    private async Task RegisterClient_ReturnsSuccessfulResult()
    {
        // Arrange
        const bool isSuccess = true;
        
        var requestDto = ClientDataProvider.GenerateRegisterClientRequestDto();
        var responseDto = ClientDataProvider.GenerateRegisterClientResponseDto();
        var command = new RegisterClientCommand(requestDto);
        ClientEntity? client = null;
        var mappedClient = new ClientEntity();
        
        _validatorMock.Setup(v => v.ValidateAsync(
                requestDto,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        
        _clientRepositoryMock.Setup(r => r.GetByEmailAsync(
                requestDto.Email,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(client);
        
        _mapperMock.Setup(m => m.Map<ClientEntity>(requestDto))
            .Returns(mappedClient);
        
        _clientRepositoryMock.Setup(r => r.AddAsync(
                mappedClient,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        
        _mapperMock.Setup(m => m.Map<RegisterClientResponseDto>(mappedClient))
            .Returns(responseDto);
        
        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.IsType<Result<RegisterClientResponseDto>>(response);
        Assert.NotNull(response.Value);
        Assert.IsType<RegisterClientResponseDto>(response.Value);
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.Equal(responseDto ,response.Value);
        Assert.Null(response.Error);
    }
    
    [Fact]
    private async Task RegisterClient_ReturnsFailedResult_WithValidationError()
    {
        // Arrange
        const bool isSuccess = false;
        
        var requestDto = ClientDataProvider.GenerateRegisterClientRequestDto();
        var command = new RegisterClientCommand(requestDto);
        
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
        Assert.IsType<Result<RegisterClientResponseDto>>(response);
        Assert.IsType<ValidationError>(response.Error);
        Assert.Null(response.Value);
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.NotNull(response.Error);
    }
    
    [Fact]
    private async Task RegisterClient_ReturnsFailedResult_AlreadyExistsError()
    {
        // Arrange
        const bool isSuccess = false;
        
        var requestDto = ClientDataProvider.GenerateRegisterClientRequestDto();
        var command = new RegisterClientCommand(requestDto);
        var client = new ClientEntity();
        
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
        Assert.IsType<Result<RegisterClientResponseDto>>(response);
        Assert.IsType<AlreadyExistsError>(response.Error);
        Assert.Null(response.Value);
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.NotNull(response.Error);
    }
    
    [Fact]
    private async Task RegisterClient_ReturnsFailedResult_InternalServerErrorError()
    {
        // Arrange
        const bool isSuccess = false;
        
        var requestDto = ClientDataProvider.GenerateRegisterClientRequestDto();
        var command = new RegisterClientCommand(requestDto);
        ClientEntity? client = null;
        var mappedClient = new ClientEntity();
        
        _validatorMock.Setup(v => v.ValidateAsync(
                requestDto,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        
        _clientRepositoryMock.Setup(r => r.GetByEmailAsync(
                requestDto.Email,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(client);
        
        _mapperMock.Setup(m => m.Map<ClientEntity>(requestDto))
            .Returns(mappedClient);
        
        _clientRepositoryMock.Setup(r => r.AddAsync(
                mappedClient,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        
        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.IsType<Result<RegisterClientResponseDto>>(response);
        Assert.IsType<InternalServerError>(response.Error);
        Assert.Null(response.Value);
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.NotNull(response.Error);
    }
}
