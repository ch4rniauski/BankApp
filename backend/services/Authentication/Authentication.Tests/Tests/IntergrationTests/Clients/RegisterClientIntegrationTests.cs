using System.Net;
using System.Net.Http.Json;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Requests;
using ch4rniauski.BankApp.Authentication.Tests.Common;
using ch4rniauski.BankApp.Authentication.Tests.Helpers.DataProviders;
using ch4rniauski.BankApp.Authentication.Tests.Helpers.UriProviders;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ch4rniauski.BankApp.Authentication.Tests.Tests.IntergrationTests.Clients;

public sealed class RegisterClientIntegrationTests : BaseIntegrationTests
{
    public RegisterClientIntegrationTests(AuthenticationWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task RegisterClient_ReturnsOk()
    {
        // Arrange
        var request = ClientDataProvider.GenerateRegisterClientRequestDto();

        var uri = AuthenticationUriProvider.GetRegisterClientUri();

        // Act
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
        
        var response = await HttpClient.PostAsJsonAsync(uri, request);

        var client = await DbContext.Clients.FirstOrDefaultAsync(c => c.Email == request.Email);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(client);
    }
    
    [Fact]
    public async Task RegisterClient_ReturnsBadRequest_WhenValidationFails()
    {
        // Arrange
        var request = new RegisterClientRequestDto(
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty);

        var uri = AuthenticationUriProvider.GetRegisterClientUri();

        // Act
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
        
        var response = await HttpClient.PostAsJsonAsync(uri, request);

        var client = await DbContext.Clients.FirstOrDefaultAsync(c => c.Email == request.Email);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Null(client);
    }
}
