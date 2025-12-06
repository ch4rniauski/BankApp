using ch4rniauski.BankApp.Authentication.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ch4rniauski.BankApp.Authentication.Tests.Common;

public abstract class BaseIntegrationTests : IClassFixture<AuthenticationWebAppFactory>
{
    protected readonly IServiceScope ServiceScope;
    protected readonly AuthenticationContext DbContext;
    protected readonly HttpClient HttpClient;
    
    protected BaseIntegrationTests(AuthenticationWebAppFactory factory)
    {
        ServiceScope = factory.Services.CreateScope();

        DbContext = ServiceScope.ServiceProvider.GetRequiredService<AuthenticationContext>();
        
        HttpClient = factory.CreateClient();
    }
}
