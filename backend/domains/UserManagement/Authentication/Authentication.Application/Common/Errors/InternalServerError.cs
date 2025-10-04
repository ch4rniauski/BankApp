namespace ch4rniauski.BankApp.Authentication.Application.Common.Errors;

public class InternalServerError : Error
{
    private const int InternalServerErrorStatusCode = 500;
    
    public InternalServerError(string message) : base(InternalServerErrorStatusCode, message)
    {
    }
}
