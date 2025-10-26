using ch4rniauski.BankApp.CreditCards.Domain.Enums;

namespace ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCard;

public sealed record TransferMoneyResponseDto(
    PaymentStatusEnum PaymentStatus,
    DateTime ProcessedAt,
    string SenderCardLast4,
    string ReceiverCardLast4,
    decimal Amount);
