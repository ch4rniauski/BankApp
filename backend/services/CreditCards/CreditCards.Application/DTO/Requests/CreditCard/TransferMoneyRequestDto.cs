namespace ch4rniauski.BankApp.CreditCards.Application.DTO.Requests.CreditCard;

public record TransferMoneyRequestDto(
    string ReceiverCardNumber,
    string SenderCardNumber,
    Guid ReceiverId,
    Guid SenderId,
    decimal Amount);
