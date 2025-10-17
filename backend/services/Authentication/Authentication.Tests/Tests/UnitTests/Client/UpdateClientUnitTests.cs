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

namespace ch4rniauski.BankApp.Authentication.Tests.Tests.UnitTests.Client;

public sealed class UpdateClientUnitTests
{
    private readonly UpdateClientCommandHandler _commandHandler;
    private readonly Mock<IClientRepository> _clientRepositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<IValidator<UpdateClientRequestDto>> _validatorMock = new();

    public UpdateClientUnitTests()
    {
        _commandHandler = new UpdateClientCommandHandler(
            _clientRepositoryMock.Object,
            _mapperMock.Object,
            _validatorMock.Object);
    }

    [Fact]
    private async Task UpdateClient_ReturnSuccessfulResult()
    {
        // Arrange
        const bool isSuccess = true;
        
        var requestDto = ClientDataProvider.GenerateUpdateClientRequestDto();
        var command = new UpdateClientCommand(Guid.NewGuid(), requestDto);
        var client = new ClientEntity();
        var responseDto = ClientDataProvider.GenerateUpdateClientResponseDto();
        
        _validatorMock.Setup(v => v.ValidateAsync(
                requestDto,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        
        _clientRepositoryMock.Setup(r => r.GetByIdAsync(
                command.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(client);

        _mapperMock.Setup(m => m.Map<ClientEntity>(requestDto))
            .Returns(client);
        
        _clientRepositoryMock.Setup(r => r.UpdateAsync(
                client,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        
        _mapperMock.Setup(m => m.Map<UpdateClientResponseDto>(client))
            .Returns(responseDto);
        
        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.IsType<Result<UpdateClientResponseDto>>(response);
        Assert.NotNull(response.Value);
        Assert.IsType<UpdateClientResponseDto>(response.Value);
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.Equal(responseDto ,response.Value);
        Assert.Null(response.Error);
    }
    
    [Fact]
    private async Task UpdateClient_ReturnFailedResult_WithValidationError()
    {
        // Arrange
        const bool isSuccess = false;
        
        var requestDto = ClientDataProvider.GenerateUpdateClientRequestDto();
        var command = new UpdateClientCommand(Guid.NewGuid(), requestDto);
        
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
        Assert.IsType<Result<UpdateClientResponseDto>>(response);
        Assert.Null(response.Value);
        Assert.IsType<ValidationError>(response.Error);
        Assert.NotNull(response.Error);
        Assert.Equal(isSuccess, response.IsSuccess);
    }
    
    [Fact]
    private async Task UpdateClient_ReturnFailedResult_WithNotFoundError()
    {
        // Arrange
        const bool isSuccess = false;
        
        var requestDto = ClientDataProvider.GenerateUpdateClientRequestDto();
        var command = new UpdateClientCommand(Guid.NewGuid(), requestDto);
        ClientEntity? client = null;
        
        _validatorMock.Setup(v => v.ValidateAsync(
                requestDto,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        
        _clientRepositoryMock.Setup(r => r.GetByIdAsync(
                command.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(client);
        
        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.IsType<Result<UpdateClientResponseDto>>(response);
        Assert.Null(response.Value);
        Assert.IsType<NotFoundError>(response.Error);
        Assert.NotNull(response.Error);
        Assert.Equal(isSuccess, response.IsSuccess);
    }
    
    [Fact]
    private async Task UpdateClient_ReturnFailedResult_WithInternalServerError()
    {
        // Arrange
        const bool isSuccess = false;
        
        var requestDto = ClientDataProvider.GenerateUpdateClientRequestDto();
        var command = new UpdateClientCommand(Guid.NewGuid(), requestDto);
        var client = new ClientEntity();
        
        _validatorMock.Setup(v => v.ValidateAsync(
                requestDto,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        
        _clientRepositoryMock.Setup(r => r.GetByIdAsync(
                command.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(client);

        _mapperMock.Setup(m => m.Map<ClientEntity>(requestDto))
            .Returns(client);
        
        _clientRepositoryMock.Setup(r => r.UpdateAsync(
                client,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        
        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.IsType<Result<UpdateClientResponseDto>>(response);
        Assert.Null(response.Value);
        Assert.IsType<InternalServerError>(response.Error);
        Assert.NotNull(response.Error);
        Assert.Equal(isSuccess, response.IsSuccess);
    }
}
