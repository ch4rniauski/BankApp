namespace ch4rniauski.BankApp.Authentication.Application.Common.Results;

public class Result<T>
{
    public bool IsSuccess { get; }
    
    public T? Value { get;  }

    private Result(bool isSuccess, T? value)
    {
       IsSuccess = isSuccess;
       Value = value;
    }
    
    public static Result<T> Success(T? value)
        => new(true, value);
    
    public static Result<T> Failure(T error)
        => new(false, error);
}
