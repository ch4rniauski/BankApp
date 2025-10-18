namespace ch4rniauski.BankApp.MoneyTransfer.Application.Common.Errors;

public abstract class Error
{
    public int StatusCode { get; }
    public string Message { get; }
    
    protected Error(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }

    public static NotFoundError NotFound(string message)
        => new NotFoundError(message);
    
    public static InternalServerError InternalError(string message)
        => new(message);
}
