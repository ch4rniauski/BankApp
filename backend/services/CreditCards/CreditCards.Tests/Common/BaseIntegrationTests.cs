using ch4rniauski.BankApp.CreditCards.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ch4rniauski.BankApp.CreditCards.Tests.Common;

public abstract class BaseIntegrationTests : IClassFixture<CreditCardsAppFactory>
{
    protected readonly CreditCardsContext DbContext;
    protected readonly HttpClient HttpClient;
    
    protected BaseIntegrationTests(CreditCardsAppFactory factory)
    {
        var serviceScope = factory.Services.CreateScope();
        
        DbContext = serviceScope.ServiceProvider.GetRequiredService<CreditCardsContext>();
        
        HttpClient = factory.CreateClient();
    }
}
