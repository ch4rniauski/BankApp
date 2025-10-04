namespace ch4rniauski.BankApp.Authentication.Application.Common.Errors;

internal class NotFoundError : Error
{
    private const int NotFoundStatusCode = 404;
    
    public NotFoundError(string message) : base(NotFoundStatusCode, message)
    {
    }
}
