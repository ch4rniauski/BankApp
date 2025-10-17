using System.Security.Cryptography;
using System.Text;
using ch4rniauski.BankApp.CreditCards.Application.Contracts.HashProviders;
using ch4rniauski.BankApp.CreditCards.Application.Hash.Settings;
using Microsoft.Extensions.Options;

namespace ch4rniauski.BankApp.CreditCards.Application.Hash.Providers;

public sealed class HmacHashProvider : IHashProvider
{
    private readonly HmacSettings _settings;

    public HmacHashProvider(IOptions<HmacSettings> options)
    {
        _settings = options.Value;
    }
    
    public string GenerateHash(string input)
    {
        var keyBytes = Encoding.UTF8.GetBytes(_settings.Key);
        var inputBytes = Encoding.UTF8.GetBytes(input);
        
        using var hmac = new HMACSHA256(keyBytes);
        
        var hashBytes = hmac.ComputeHash(inputBytes);
        
        return Convert.ToBase64String(hashBytes);
    }

    public bool VerifyHash(string input, string hash)
        => GenerateHash(input) == hash;
}
