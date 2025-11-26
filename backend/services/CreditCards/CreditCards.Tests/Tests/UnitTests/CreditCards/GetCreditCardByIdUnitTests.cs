using AutoMapper;
using ch4rniauski.BankApp.CreditCards.Application.Common.Results;
using ch4rniauski.BankApp.CreditCards.Application.Contracts.Repositories;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCards;
using ch4rniauski.BankApp.CreditCards.Application.UseCases.Queries.CreditCards;
using ch4rniauski.BankApp.CreditCards.Application.UseCases.QueryHandlers.CreditCards;
using ch4rniauski.BankApp.CreditCards.Domain.Entities;
using ch4rniauski.BankApp.CreditCards.Tests.Helpers.DataProviders;
using Moq;
using Xunit;

namespace ch4rniauski.BankApp.CreditCards.Tests.Tests.UnitTests.CreditCards;

public sealed class GetCreditCardByIdUnitTests
{
    private readonly GetCreditCardByIdQueryHandler _queryHandler;
    private readonly Mock<ICreditCardRepository> _creditCardRepositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    public GetCreditCardByIdUnitTests()
    {
        _queryHandler = new GetCreditCardByIdQueryHandler(
            _creditCardRepositoryMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    private async Task GetCreditCardById_ReturnsSuccessfulResult()
    {
        // Arrange
        const bool isSuccess = true;
        var request = new GetCreditCardByIdQuery(Guid.NewGuid());
        var creditCard = new CreditCardEntity();
        var result = CreditCardDataProvider.GenerateGetCreditCardResponseDto();
        
        _creditCardRepositoryMock.Setup(c => c.GetByIdAsync(
                request.CardId, 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(creditCard);

        _mapperMock.Setup(m => m.Map<GetCreditCardResponseDto>(creditCard))
            .Returns(result);
        
        // Act
        var response = await _queryHandler.Handle(request, CancellationToken.None);
        
        // Assert
        Assert.IsType<Result<GetCreditCardResponseDto>>(response);
        Assert.NotNull(response.Value);
        Assert.IsType<GetCreditCardResponseDto>(response.Value);
        Assert.Null(response.Error);
        Assert.Equal(isSuccess, response.IsSuccess);
    }
}
