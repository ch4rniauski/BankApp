namespace ch4rniauski.BankApp.Authentication.Application.Common.Errors;

public class AlreadyExistsError : Error
{
    private const int AlreadyExistsStatusCode = 409;
    
    public AlreadyExistsError(string message) : base(AlreadyExistsStatusCode, message)
    {
    }
}
