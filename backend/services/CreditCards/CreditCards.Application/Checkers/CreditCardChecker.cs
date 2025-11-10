using ch4rniauski.BankApp.CreditCards.Application.Common.Errors;
using ch4rniauski.BankApp.CreditCards.Application.Common.Results;
using ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCards;
using ch4rniauski.BankApp.CreditCards.Domain.Entities;

namespace ch4rniauski.BankApp.CreditCards.Application.Checkers;

internal static class CreditCardChecker
{
    public static Result<TransferMoneyResponseDto>? CheckCreditCard(
        CreditCardEntity? creditCard,
        string cardNumber)
    {
        if (creditCard is null)
        {
            return Result<TransferMoneyResponseDto>
                .Failure(Error.NotFound(
                    $"Credit Card with number {cardNumber} does not exist"
                ));
        }
        
        return null;
    }
}
