using AutoMapper;
using ch4rniauski.BankApp.CreditCards.Application.Common.Errors;
using ch4rniauski.BankApp.CreditCards.Application.Common.Results;
using ch4rniauski.BankApp.CreditCards.Application.Contracts.RabbitMQ;
using ch4rniauski.BankApp.CreditCards.Application.Contracts.Repositories;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCards;
using ch4rniauski.BankApp.CreditCards.Application.UseCases.CommandHandlers.CreditCards;
using ch4rniauski.BankApp.CreditCards.Application.UseCases.Commands.CreditCards;
using ch4rniauski.BankApp.CreditCards.Domain.Entities;
using ch4rniauski.BankApp.CreditCards.Domain.Messages;
using ch4rniauski.BankApp.CreditCards.Tests.Helpers.DataProviders;
using Moq;
using Xunit;

namespace ch4rniauski.BankApp.CreditCards.Tests.Tests.UnitTests.CreditCards;

public sealed class TransferMoneyToCardUnitTests
{
    private readonly TransferMoneyToCardCommandHandler _commandHandler;
    private readonly Mock<ICreditCardRepository> _creditCardRepositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<INotificationProducer> _notificationProducerMock = new();

    public TransferMoneyToCardUnitTests()
    {
        _commandHandler = new TransferMoneyToCardCommandHandler(
            _creditCardRepositoryMock.Object,
            _mapperMock.Object,
            _notificationProducerMock.Object);
    }

    [Fact]
    private async Task TransferMoneyToCard_ReturnsSuccessfulResult()
    {
        // Arrange
        const bool isSuccess = true;
        
        var request = TransferMoneyDataProvider.GenerateTransferMoneyRequestDto();
        var command = new TransferMoneyToCardCommand(request);
        var creditCard = new CreditCardEntity
        {
            Balance = request.Amount
        };
        
        _creditCardRepositoryMock.Setup(c => c.GetCardByNumberAsync(
                request.ReceiverCardNumber,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(creditCard);
        
        _creditCardRepositoryMock.Setup(c => c.GetCardByNumberAsync(
                request.SenderCardNumber,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(creditCard);
        
        _creditCardRepositoryMock.Setup(c => c.TransferMoneyAsync(
                request.SenderCardNumber,
                request.ReceiverCardNumber,
                request.Amount,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _notificationProducerMock.Setup(n => n.PublishAsync(
                It.IsAny<NotificationMessage>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
            
        
        _mapperMock.Setup(m => m.Map<TransferMoneyResponseDto>(request))
            .Returns(new TransferMoneyResponseDto());
        
        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.IsType<Result<TransferMoneyResponseDto>>(response);
        Assert.NotNull(response.Value);
        Assert.IsType<TransferMoneyResponseDto>(response.Value);
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.Null(response.Error);
    }
    
    [Fact]
    private async Task TransferMoneyToCard_ReturnsFailedResult_WithNotFoundError_WhenReceiverCardWasNotFound()
    {
        // Arrange
        const bool isSuccess = false;
        
        var request = TransferMoneyDataProvider.GenerateTransferMoneyRequestDto();
        var command = new TransferMoneyToCardCommand(request);
        CreditCardEntity? creditCard = null;
        
        _creditCardRepositoryMock.Setup(c => c.GetCardByNumberAsync(
                request.ReceiverCardNumber,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(creditCard);
        
        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.IsType<Result<TransferMoneyResponseDto>>(response);
        Assert.Null(response.Value);
        Assert.NotNull(response.Error);
        Assert.IsType<NotFoundError>(response.Error);
        Assert.Equal(isSuccess, response.IsSuccess);
    }
    
    [Fact]
    private async Task TransferMoneyToCard_ReturnsFailedResult_WithNotFoundError_WhenSenderCardWasNotFound()
    {
        // Arrange
        const bool isSuccess = false;
        
        var request = TransferMoneyDataProvider.GenerateTransferMoneyRequestDto();
        var command = new TransferMoneyToCardCommand(request);
        var receiverCreditCard = new CreditCardEntity();
        CreditCardEntity? senderCreditCard = null;
        
        _creditCardRepositoryMock.Setup(c => c.GetCardByNumberAsync(
                request.ReceiverCardNumber,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(receiverCreditCard);
        
        _creditCardRepositoryMock.Setup(c => c.GetCardByNumberAsync(
                request.SenderCardNumber,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(senderCreditCard);
        
        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.IsType<Result<TransferMoneyResponseDto>>(response);
        Assert.Null(response.Value);
        Assert.NotNull(response.Error);
        Assert.IsType<NotFoundError>(response.Error);
        Assert.Equal(isSuccess, response.IsSuccess);
    }
    
    [Fact]
    private async Task TransferMoneyToCard_ReturnsFailedResult_WithFundsLackError()
    {
        // Arrange
        const bool isSuccess = false;
        
        var request = TransferMoneyDataProvider.GenerateTransferMoneyRequestDto();
        var command = new TransferMoneyToCardCommand(request);
        var creditCard = new CreditCardEntity
        {
            Balance = request.Amount - 1
        };
        
        _creditCardRepositoryMock.Setup(c => c.GetCardByNumberAsync(
                request.ReceiverCardNumber,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(creditCard);
        
        _creditCardRepositoryMock.Setup(c => c.GetCardByNumberAsync(
                request.SenderCardNumber,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(creditCard);
        
        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.IsType<Result<TransferMoneyResponseDto>>(response);
        Assert.Null(response.Value);
        Assert.NotNull(response.Error);
        Assert.IsType<FundsLackError>(response.Error);
        Assert.Equal(isSuccess, response.IsSuccess);
    }
    
    [Fact]
    private async Task TransferMoneyToCard_ReturnsFailedResult_WithInternalError()
    {
        // Arrange
        const bool isSuccess = false;
        
        var request = TransferMoneyDataProvider.GenerateTransferMoneyRequestDto();
        var command = new TransferMoneyToCardCommand(request);
        var creditCard = new CreditCardEntity
        {
            Balance = request.Amount
        };
        
        _creditCardRepositoryMock.Setup(c => c.GetCardByNumberAsync(
                request.ReceiverCardNumber,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(creditCard);
        
        _creditCardRepositoryMock.Setup(c => c.GetCardByNumberAsync(
                request.SenderCardNumber,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(creditCard);
        
        _creditCardRepositoryMock.Setup(c => c.TransferMoneyAsync(
                request.SenderCardNumber,
                request.ReceiverCardNumber,
                request.Amount,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        
        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.IsType<Result<TransferMoneyResponseDto>>(response);
        Assert.Null(response.Value);
        Assert.NotNull(response.Error);
        Assert.IsType<InternalServerError>(response.Error);
        Assert.Equal(isSuccess, response.IsSuccess);
    }
}
