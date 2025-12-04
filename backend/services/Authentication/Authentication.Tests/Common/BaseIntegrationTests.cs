using ch4rniauski.BankApp.Authentication.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ch4rniauski.BankApp.Authentication.Tests.Common;

internal abstract class BaseIntegrationTests : IClassFixture<AuthenticationWebAppFactory>
{
    protected readonly IServiceScope ServiceScope;
    protected readonly AuthenticationContext DbContext;
    protected readonly IMediator Mediator;
    
    protected BaseIntegrationTests(AuthenticationWebAppFactory factory)
    {
        ServiceScope = factory.Services.CreateScope();

        DbContext = ServiceScope.ServiceProvider.GetRequiredService<AuthenticationContext>();
        Mediator = ServiceScope.ServiceProvider.GetRequiredService<IMediator>();
    }
}
