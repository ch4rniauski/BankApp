namespace ch4rniauski.BankApp.MoneyTransfer.Application.DTO.Requests.Payments;

public sealed record TransferMoneyRequestDto(
    string SenderCardNumber,
    string ReceiverCardNumber,
    Guid SenderId,
    Guid ReceiverId,
    decimal Amount,
    string Currency);
