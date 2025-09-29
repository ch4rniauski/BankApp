using ch4rniauski.BankApp.Authentication.Domain.Entities;

namespace ch4rniauski.BankApp.Authentication.Application.Contracts.Jwt;

public interface ITokenProvider
{
    string GenerateAccessToken(ClientEntity client);
    string GenerateRefreshToken();
}