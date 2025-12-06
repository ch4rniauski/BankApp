using System.Net;
using System.Net.Http.Json;
using ch4rniauski.BankApp.Authentication.Tests.Common;
using ch4rniauski.BankApp.Authentication.Tests.Helpers.DataProviders;
using ch4rniauski.BankApp.Authentication.Tests.Helpers.UriProviders;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ch4rniauski.BankApp.Authentication.Tests.Tests.IntergrationTests.Clients;

public sealed class DeleteClientIntegrationTests : BaseIntegrationTests
{
    public DeleteClientIntegrationTests(AuthenticationWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task DeleteClient_ReturnsOk()
    {
        // Arrange
        var clientEntity = ClientDataProvider.GenerateClientEntity();

        var uri = AuthenticationUriProvider.GetDeleteClientUri(clientEntity.Id);

        // Act
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
        
        await DbContext.Clients.AddAsync(clientEntity);
        await DbContext.SaveChangesAsync();
        
        var response = await HttpClient.DeleteAsync(uri);

        var client = await DbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientEntity.Id);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Null(client);
    }
    
    [Fact]
    public async Task DeleteClient_ReturnsNotFound_WhenClientDoesntExist()
    {
        // Arrange
        var id = Guid.NewGuid();
        var uri = AuthenticationUriProvider.GetDeleteClientUri(id);

        // Act
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
        
        var response = await HttpClient.DeleteAsync(uri);

        var client = await DbContext.Clients.FirstOrDefaultAsync(c => c.Id == id);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Null(client);
    }
}
