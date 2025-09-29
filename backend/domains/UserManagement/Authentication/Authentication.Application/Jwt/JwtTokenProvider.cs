using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ch4rniauski.BankApp.Authentication.Application.Contracts.Jwt;
using ch4rniauski.BankApp.Authentication.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace ch4rniauski.BankApp.Authentication.Application.Jwt;

internal sealed class JwtTokenProvider : ITokenProvider
{
    private readonly JwtSettings _settings;
    
    public JwtTokenProvider(IOptions<JwtSettings> options)
    {
        _settings = options.Value;
    }
    
    public string GenerateAccessToken(ClientEntity client)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString())
            ]),
            Expires = DateTime.UtcNow.AddMinutes(_settings.ExpiresInMinutes),
            SigningCredentials = credentials
        };
        
        var handler = new JsonWebTokenHandler();
        
        var token = handler.CreateToken(tokenDescriptor);

        return token;
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}