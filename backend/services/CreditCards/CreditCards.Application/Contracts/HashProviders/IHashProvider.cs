namespace ch4rniauski.BankApp.CreditCards.Application.Contracts.HashProviders;

public interface IHashProvider
{
    string GenerateHash(string input);
    bool VerifyHash(string input, string hash);
}
