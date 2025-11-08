namespace ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCards;

public sealed record CreateCreditCardResponseDto(
    Guid Id,
    string CardNumber,
    string CardType,
    byte ExpiryMonth,
    short ExpiryYear,
    string CardHolderName);
