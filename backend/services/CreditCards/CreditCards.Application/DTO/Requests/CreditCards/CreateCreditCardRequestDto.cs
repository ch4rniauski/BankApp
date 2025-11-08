using ch4rniauski.BankApp.CreditCards.Domain.Enums;

namespace ch4rniauski.BankApp.CreditCards.Application.DTO.Requests.CreditCards;

public sealed record CreateCreditCardRequestDto(
    CreditCardTypeEnum CardType,
    Guid CardHolderId);
