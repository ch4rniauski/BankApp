namespace ch4rniauski.BankApp.MoneyTransfer.Application.Common.Errors;

public class InternalServerError : Error
{
    private const int InternalServerErrorStatusCode = 500;
    
    public InternalServerError(string message) : base(InternalServerErrorStatusCode, message)
    {
    }
}
