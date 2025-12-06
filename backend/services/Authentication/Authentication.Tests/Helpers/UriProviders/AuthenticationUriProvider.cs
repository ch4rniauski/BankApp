namespace ch4rniauski.BankApp.Authentication.Tests.Helpers.UriProviders;

internal static class AuthenticationUriProvider
{
    private const string BaseUri = "api/clients";

    public static string GetRegisterClientUri()
        => $"{BaseUri}";
    
    public static string GetDeleteClientUri(Guid clientId)
        => $"{BaseUri}/{clientId}";
    
    public static string GetUpdateClientUri(Guid clientId)
        => $"{BaseUri}/{clientId}";
    
    public static string GetLoginClientUri()
        => $"{BaseUri}/login";
}
