namespace ch4rniauski.BankApp.MoneyTransfer.Application.DTO.Requests.Payments;

public sealed record TransferMoneyRequestDto(
    string SenderCardNumber,
    string ReceiverCardNumber,
    decimal Amount,
    string Currency,
    string? Description);
