namespace ch4rniauski.BankApp.CreditCards.Tests.Helpers.UriProviders;

internal static class CreditCardsUriProvider
{
    private const string BaseUri = "api/creditcards";

    public static string GetCreditCardsByClientIdUri(Guid clientId)
        => $"{BaseUri}/clients/{clientId}";
}
