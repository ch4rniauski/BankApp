namespace ch4rniauski.BankApp.CreditCards.Application.Common.Errors;

public sealed class FundsLackError : Error
{
    private const int FundsLackErrorStatusCode = 409;
    
    public FundsLackError(string message) : base(FundsLackErrorStatusCode, message)
    {
    }
}
