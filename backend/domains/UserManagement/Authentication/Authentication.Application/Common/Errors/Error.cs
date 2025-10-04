namespace ch4rniauski.BankApp.Authentication.Application.Common.Errors;

internal abstract class Error
{
    public int StatusCode { get; }
    
    public string Message { get; }

    protected Error(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }
    
    public static ValidationError FailedValidation(string message)
        => new ValidationError(message);
    
    public static NotFoundError NotFound(string message)
        => new NotFoundError(message);
}
