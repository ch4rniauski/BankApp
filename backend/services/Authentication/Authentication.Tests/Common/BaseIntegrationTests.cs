using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ch4rniauski.BankApp.Authentication.Tests.Common;

internal abstract class BaseIntegrationTests : IClassFixture<AuthenticationWebAppFactory>
{
    protected readonly IServiceScope ServiceScope;
    
    protected BaseIntegrationTests(AuthenticationWebAppFactory factory)
    {
        ServiceScope = factory.Services.CreateScope();
    }
}
