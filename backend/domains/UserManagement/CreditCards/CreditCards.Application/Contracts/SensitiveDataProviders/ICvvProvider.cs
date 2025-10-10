namespace ch4rniauski.BankApp.CreditCards.Application.Contracts.SensitiveDataProviders;

public interface ICvvProvider
{
    string GenerateCvvCode();
}
