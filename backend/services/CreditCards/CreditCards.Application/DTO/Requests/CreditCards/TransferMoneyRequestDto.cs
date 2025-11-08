namespace ch4rniauski.BankApp.CreditCards.Application.DTO.Requests.CreditCards;

public record TransferMoneyRequestDto(
    string ReceiverCardNumber,
    string SenderCardNumber,
    Guid ReceiverId,
    Guid SenderId,
    decimal Amount);
