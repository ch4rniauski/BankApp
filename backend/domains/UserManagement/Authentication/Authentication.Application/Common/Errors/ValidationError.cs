namespace ch4rniauski.BankApp.Authentication.Application.Common.Errors;

internal class ValidationError : Error
{
    private const int FailedValidationStatusCode = 400;
    
    public ValidationError(string message) : base(FailedValidationStatusCode, message)
    {
    }
}
