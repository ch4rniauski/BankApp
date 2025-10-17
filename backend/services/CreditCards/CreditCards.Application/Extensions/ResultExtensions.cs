using ch4rniauski.BankApp.CreditCards.Application.Common.Errors;
using ch4rniauski.BankApp.CreditCards.Application.Common.Results;

namespace ch4rniauski.BankApp.CreditCards.Application.Extensions;

public static class ResultExtensions
{
    public static TResult Match<TValue, TResult>(
        this Result<TValue> result,
        Func<TValue, TResult> onSuccess,
        Func<Error, TResult> onFailure)
    {
        return result.IsSuccess
            ? onSuccess(result.Value!)
            : onFailure(result.Error!);
    }
}
