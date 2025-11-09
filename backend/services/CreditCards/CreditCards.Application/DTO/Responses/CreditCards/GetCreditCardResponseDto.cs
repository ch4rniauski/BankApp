namespace ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCards;

public sealed record GetCreditCardResponseDto(
    Guid Id,
    string CardNumber,
    string CardType,
    decimal Balance,
    byte ExpirationMonth,
    short ExpirationYear,
    string CardHolderName);
