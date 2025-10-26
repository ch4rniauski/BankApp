using ch4rniauski.BankApp.MoneyTransfer.Domain.Enums;

namespace ch4rniauski.BankApp.MoneyTransfer.Application.DTO.Responses.Payments;

public sealed record TransferMoneyResponseDto(
    decimal Amount,
    string Currency,
    PaymentStatusEnum Status,
    string SenderCardLast4,
    string ReceiverCardLast4);
