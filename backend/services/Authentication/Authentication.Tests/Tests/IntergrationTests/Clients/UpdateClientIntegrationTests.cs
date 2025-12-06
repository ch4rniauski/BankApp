using System.Net;
using System.Net.Http.Json;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Requests;
using ch4rniauski.BankApp.Authentication.Tests.Common;
using ch4rniauski.BankApp.Authentication.Tests.Helpers.DataProviders;
using ch4rniauski.BankApp.Authentication.Tests.Helpers.UriProviders;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ch4rniauski.BankApp.Authentication.Tests.Tests.IntergrationTests.Clients;

public sealed class UpdateClientIntegrationTests : BaseIntegrationTests
{
    public UpdateClientIntegrationTests(AuthenticationWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task UpdateClient_ReturnsOk()
    {
        // Arrange
        var clientEntity = ClientDataProvider.GenerateClientEntity();
        var request = ClientDataProvider.GenerateUpdateClientRequestDto();

        var uri = AuthenticationUriProvider.GetUpdateClientUri(clientEntity.Id);

        // Act
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
        
        await DbContext.Clients.AddAsync(clientEntity);
        await DbContext.SaveChangesAsync();
        
        var response = await HttpClient.PutAsJsonAsync(uri, request);

        var client = await DbContext.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email == request.Email);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(client);
        Assert.Equal(clientEntity.Id, client.Id);
        Assert.Equal(client.FirstName, request.FirstName);
        Assert.Equal(client.LastName, request.LastName);
        Assert.Equal(client.PhoneNumber, request.PhoneNumber);
        Assert.Equal(client.Email, request.Email);
    }
    
    [Fact]
    public async Task UpdateClient_ReturnsBadRequest_WhenValidationFails()
    {
        // Arrange
        var request = new UpdateClientRequestDto(
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty);

        var uri = AuthenticationUriProvider.GetUpdateClientUri(Guid.NewGuid());

        // Act
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
        
        var response = await HttpClient.PutAsJsonAsync(uri, request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task UpdateClient_ReturnsNotFound_WhenClientDoesntExist()
    {
        // Arrange
        var request = ClientDataProvider.GenerateUpdateClientRequestDto();

        var uri = AuthenticationUriProvider.GetUpdateClientUri(Guid.NewGuid());

        // Act
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
        
        var response = await HttpClient.PutAsJsonAsync(uri, request);

        var client = await DbContext.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email == request.Email);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Null(client);
    }
}
