using System.Net;
using System.Net.Http.Json;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Requests;
using ch4rniauski.BankApp.Authentication.Tests.Common;
using ch4rniauski.BankApp.Authentication.Tests.Helpers.DataProviders;
using ch4rniauski.BankApp.Authentication.Tests.Helpers.UriProviders;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ch4rniauski.BankApp.Authentication.Tests.Tests.IntergrationTests.Clients;

public sealed class UpdateAccessTokenIntegrationTests : BaseIntegrationTests
{
    public UpdateAccessTokenIntegrationTests(AuthenticationWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task UpdateAccessToken_ReturnsOk()
    {
        // Arrange
        const string refreshToken = "refreshToken";
        
        var clientEntity = ClientDataProvider.GenerateClientEntity();
        clientEntity.RefreshToken = refreshToken;
        
        var request = new UpdateAccessTokenRequestDto(
            refreshToken,
            clientEntity.Id.ToString());

        var uri = AuthenticationUriProvider.GetUpdateAccessTokenUri();

        // Act
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
        
        await DbContext.Clients.AddAsync(clientEntity);
        await DbContext.SaveChangesAsync();
        
        var response = await HttpClient.PostAsJsonAsync(uri, request);
        
        var client = await DbContext.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == clientEntity.Id);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(client);
    }
    
    [Fact]
    public async Task UpdateAccessToken_ReturnsNotFound_WhenIdWasNotProvided()
    {
        // Arrange
        const string refreshToken = "refreshToken";
        
        var request = new UpdateAccessTokenRequestDto(
            refreshToken,
            null);

        var uri = AuthenticationUriProvider.GetUpdateAccessTokenUri();

        // Act
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
        
        var response = await HttpClient.PostAsJsonAsync(uri, request);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Fact]
    public async Task UpdateAccessToken_ReturnsBadRequest_WhenIdTypeIsInvalid()
    {
        // Arrange
        const string refreshToken = "refreshToken";
        const string incorrectGuid = "Incorrect GUID";
        
        var request = new UpdateAccessTokenRequestDto(
            refreshToken,
            incorrectGuid);

        var uri = AuthenticationUriProvider.GetUpdateAccessTokenUri();

        // Act
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
        
        var response = await HttpClient.PostAsJsonAsync(uri, request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task UpdateAccessToken_ReturnsNotFound_WhenClientDoesNotExist()
    {
        // Arrange
        const string refreshToken = "refreshToken";
        var id = Guid.NewGuid();
        
        var request = new UpdateAccessTokenRequestDto(
            refreshToken,
            id.ToString());

        var uri = AuthenticationUriProvider.GetUpdateAccessTokenUri();

        // Act
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
        
        var response = await HttpClient.PostAsJsonAsync(uri, request);
        
        var client = await DbContext.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Null(client);
    }
    
    [Fact]
    public async Task UpdateAccessToken_ReturnsUnauthorized_WhenRefreshTokenIsNotValid()
    {
        // Arrange
        const string refreshToken = "refreshToken";
        
        var clientEntity = ClientDataProvider.GenerateClientEntity();
        clientEntity.RefreshToken = string.Empty;
        
        var request = new UpdateAccessTokenRequestDto(
            refreshToken,
            clientEntity.Id.ToString());

        var uri = AuthenticationUriProvider.GetUpdateAccessTokenUri();

        // Act
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
        
        await DbContext.Clients.AddAsync(clientEntity);
        await DbContext.SaveChangesAsync();
        
        var response = await HttpClient.PostAsJsonAsync(uri, request);
        
        var client = await DbContext.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == clientEntity.Id);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        Assert.NotNull(client);
    }
}
