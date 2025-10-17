using ch4rniauski.BankApp.CreditCards.Application.Contracts.SensitiveDataProviders;

namespace ch4rniauski.BankApp.CreditCards.Application.SensitiveDataProviders;

public class PinCodeProvider : IPinCodeProvider
{
    public string GeneratePinCode() =>
        string.Concat(
            Enumerable.Range(0, 4)
                .Select(_ => Random.Shared.Next(0, 10).ToString())
            );
}
