using System.Net;
using System.Net.Http.Json;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCards;
using ch4rniauski.BankApp.CreditCards.Tests.Common;
using ch4rniauski.BankApp.CreditCards.Tests.Helpers.DataProviders;
using ch4rniauski.BankApp.CreditCards.Tests.Helpers.UriProviders;
using Xunit;

namespace ch4rniauski.BankApp.CreditCards.Tests.Tests.IntegrationTests.CreditCards;

public class GetCreditCardsByClientIdIntegrationTest : BaseIntegrationTests
{
    public GetCreditCardsByClientIdIntegrationTest(CreditCardsAppFactory factory) : base(factory)
    {
    }

    [Fact]
    private async Task GetCreditCardsByClientId_ReturnsOk()
    {
        // Arrange
        const int cardsAmount = 5;
        
        var cardHolderId = Guid.NewGuid();
        var creditCards = CreditCardDataProvider.GenerateListOfCreditCardEntity(cardsAmount, cardHolderId);
        
        var uri = CreditCardsUriProvider.GetCreditCardsByClientIdUri(cardHolderId);
        
        // Act
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
        
        await DbContext.CreditCards.AddRangeAsync(creditCards);
        
        await DbContext.SaveChangesAsync();

        var response = await HttpClient.GetAsync(uri);
        var result = await response.Content.ReadFromJsonAsync<IList<GetCreditCardResponseDto>>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }
}
