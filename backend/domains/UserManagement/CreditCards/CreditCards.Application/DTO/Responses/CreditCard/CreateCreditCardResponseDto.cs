namespace ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCard;

public sealed record CreateCreditCardResponseDto(
    Guid Id,
    string CardNumber,
    string CardType,
    byte ExpiryMonth,
    short ExpiryYear,
    string CardHolderName);
