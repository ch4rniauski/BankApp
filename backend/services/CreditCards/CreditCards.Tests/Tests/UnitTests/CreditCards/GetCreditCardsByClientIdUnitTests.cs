using ch4rniauski.BankApp.CreditCards.Application.Contracts.Repositories;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCards;
using ch4rniauski.BankApp.CreditCards.Application.UseCases.Queries.CreditCards;
using ch4rniauski.BankApp.CreditCards.Application.UseCases.QueryHandlers.CreditCards;
using ch4rniauski.BankApp.CreditCards.Tests.Helpers.DataProviders;
using Moq;
using Xunit;

namespace ch4rniauski.BankApp.CreditCards.Tests.Tests.UnitTests.CreditCards;

public sealed class GetCreditCardsByClientIdUnitTests
{
    private readonly GetCreditCardsByClientIdQueryHandler _queryHandler;
    private readonly Mock<ICreditCardRepository> _creditCardRepositoryMock = new();

    public GetCreditCardsByClientIdUnitTests()
    {
        _queryHandler = new GetCreditCardsByClientIdQueryHandler(_creditCardRepositoryMock.Object);
    }

    [Fact]
    private async Task GetCreditCardsByClient_ReturnsListOfCreditCards()
    {
        // Arrange
        var request = new GetCreditCardsByClientIdQuery(Guid.NewGuid());
        var response = CreditCardDataProvider.GenerateListOfGetCreditCardResponseDto(1);

        _creditCardRepositoryMock.Setup(c => c.GetCardsByClientId<GetCreditCardResponseDto>(
                request.ClientId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        
        // Act
        var result = await _queryHandler.Handle(request, CancellationToken.None);
        
        // Assert
        Assert.IsType<List<GetCreditCardResponseDto>>(result);
    }
}
