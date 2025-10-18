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
}
