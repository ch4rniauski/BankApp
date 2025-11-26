using AutoMapper;
using ch4rniauski.BankApp.CreditCards.Application.Common.Results;
using ch4rniauski.BankApp.CreditCards.Application.Contracts.Repositories;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCards;
using ch4rniauski.BankApp.CreditCards.Application.UseCases.CommandHandlers.CreditCards;
using ch4rniauski.BankApp.CreditCards.Application.UseCases.Commands.CreditCards;
using ch4rniauski.BankApp.CreditCards.Domain.Entities;
using ch4rniauski.BankApp.CreditCards.Tests.Helpers.DataProviders;
using Moq;
using Xunit;

namespace ch4rniauski.BankApp.CreditCards.Tests.Tests.UnitTests.CreditCards;

public class TransferMoneyToCardUnitTests
{
    private readonly TransferMoneyToCardCommandHandler _commandHandler;
    private readonly Mock<ICreditCardRepository> _creditCardRepositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    public TransferMoneyToCardUnitTests()
    {
        _commandHandler = new TransferMoneyToCardCommandHandler(
            _creditCardRepositoryMock.Object,
            _mapperMock.Object);
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
}
