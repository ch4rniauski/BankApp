using ch4rniauski.BankApp.CreditCards.Domain.Enums;

namespace ch4rniauski.BankApp.CreditCards.Application.DTO.Requests.CreditCard;

public sealed record CreateCreditCardRequestDto(
    CreditCardTypeEnum CardType,
    Guid CardHolderId);
