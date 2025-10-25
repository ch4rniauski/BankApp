using ch4rniauski.BankApp.MoneyTransfer.Domain.Enums;

namespace ch4rniauski.BankApp.MoneyTransfer.Application.DTO.Common.Payments;

public sealed record IntermediatePaymentEntityDto(
    Guid Id,
    DateTime ProcessedAt,
    decimal Amount,
    PaymentStatusEnum Status,
    string ReceiverCardLast4,
    string SenderCardLast4);
