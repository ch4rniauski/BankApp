namespace ch4rniauski.BankApp.Authentication.Application.Jwt;

internal sealed class JwtSettings
{
    public string SecurityKey { get; set; } = string.Empty;
    public int ExpiresInMinutes { get; set; }
}
