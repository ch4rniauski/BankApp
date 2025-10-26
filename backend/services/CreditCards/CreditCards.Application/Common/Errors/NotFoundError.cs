namespace ch4rniauski.BankApp.CreditCards.Application.Common.Errors;

public sealed class NotFoundError : Error
{
    private const int NotFoundStatusCode = 404;
    
    public NotFoundError(string message) : base(NotFoundStatusCode, message)
    {
    }
}
