namespace ch4rniauski.BankApp.Authentication.Application.Common.Errors;

public abstract class Error
{
    public int StatusCode { get; }
    
    public string Message { get; }

    protected Error(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }
    
    public static ValidationError FailedValidation(string message)
        => new(message);
    
    public static NotFoundError NotFound(string message)
        => new(message);
    
    public static AlreadyExistsError AlreadyExists(string message)
        => new(message);
    
    public static InternalServerError InternalError(string message)
        => new(message);
}
