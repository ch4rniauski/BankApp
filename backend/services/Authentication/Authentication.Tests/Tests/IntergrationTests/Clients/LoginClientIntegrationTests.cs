using System.Net;
using System.Net.Http.Json;
using ch4rniauski.BankApp.Authentication.Domain.Entities;
using ch4rniauski.BankApp.Authentication.Tests.Common;
using ch4rniauski.BankApp.Authentication.Tests.Helpers.DataProviders;
using ch4rniauski.BankApp.Authentication.Tests.Helpers.UriProviders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ch4rniauski.BankApp.Authentication.Tests.Tests.IntergrationTests.Clients;

public sealed class LoginClientIntegrationTests : BaseIntegrationTests
{
    public LoginClientIntegrationTests(AuthenticationWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task LoginClient_ReturnsOk()
    {
        // Arrange
        var clientEntity = ClientDataProvider.GenerateClientEntity();
        var request = ClientDataProvider.GenerateLoginClientRequestDto();

        clientEntity.PasswordHash = new PasswordHasher<ClientEntity>().HashPassword(clientEntity, request.Password);
        clientEntity.Email = request.Email;
        
        var uri = AuthenticationUriProvider.GetLoginClientUri();

        // Act
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
        
        await DbContext.Clients.AddAsync(clientEntity);
        await DbContext.SaveChangesAsync();
        
        var response = await HttpClient.PostAsJsonAsync(uri, request);
        
        var client = await DbContext.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email == request.Email);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(client);
        Assert.NotNull(client.RefreshToken);
    }
    
    [Fact]
    public async Task LoginClient_ReturnsBadRequest_WhenValidationFails()
    {
        // Arrange
        var request = ClientDataProvider.GenerateLoginClientRequestDto();
        
        var uri = AuthenticationUriProvider.GetLoginClientUri();

        // Act
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
        
        var response = await HttpClient.PostAsJsonAsync(uri, request);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Fact]
    public async Task LoginClient_ReturnsUnauthorized_WhenPasswordIsInvalid()
    {
        // Arrange
        var clientEntity = ClientDataProvider.GenerateClientEntity();
        var request = ClientDataProvider.GenerateLoginClientRequestDto();
        
        var correctPassword = $"{request.Password} correct";
        
        clientEntity.PasswordHash = new PasswordHasher<ClientEntity>().HashPassword(clientEntity, correctPassword);
        clientEntity.Email = request.Email;
        
        var uri = AuthenticationUriProvider.GetLoginClientUri();

        // Act
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
        
        await DbContext.Clients.AddAsync(clientEntity);
        await DbContext.SaveChangesAsync();
        
        var response = await HttpClient.PostAsJsonAsync(uri, request);
        
        var client = await DbContext.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email == request.Email);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        Assert.NotNull(client);
        Assert.Null(client.RefreshToken);
    }
}
