using ch4rniauski.BankApp.CreditCards.Application.Contracts.SensitiveDataProviders;

namespace ch4rniauski.BankApp.CreditCards.Application.SensitiveDataProviders;

public class CvvProvider : ICvvProvider
{
    public string GenerateCvvCode() =>
        string.Concat(
            Enumerable.Range(0, 3)
                .Select(_ => Random.Shared.Next(0, 10).ToString())
            );
}
