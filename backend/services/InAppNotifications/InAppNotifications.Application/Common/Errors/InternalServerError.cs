namespace ch4rniauski.BankApp.InAppNotifications.Application.Common.Errors;

public sealed class InternalServerError : Error
{
    private const int InternalServerErrorStatusCode = 500;
    
    public InternalServerError(string message) : base(InternalServerErrorStatusCode, message)
    {
    }
}
