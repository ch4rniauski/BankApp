using Xunit;

namespace ch4rniauski.BankApp.CreditCards.Tests.Common;

public abstract class BaseIntegrationTests : IClassFixture<CreditCardsAppFactory>
{
    protected BaseIntegrationTests()
    {
        
    }
}
