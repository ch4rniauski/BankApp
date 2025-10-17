using ch4rniauski.BankApp.CreditCards.Application.Common.Errors;

namespace ch4rniauski.BankApp.CreditCards.Application.Common.Results;

public class Result<T>
{
    public bool IsSuccess { get; }
    
    public T? Value { get; }
    
    public Error? Error { get; }

    private Result(
        bool isSuccess,
        T? value,
        Error? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }
    
    public static Result<T> Success(T value)
        => new(
            isSuccess: true,
            value: value,
            error: null);
    
    public static Result<T> Failure(Error error)
        => new(
            isSuccess: false,
            value: default,
            error: error);
}
