using ch4rniauski.BankApp.CreditCards.Domain.Enums;

namespace ch4rniauski.BankApp.CreditCards.Application.DTO.Responses.CreditCards;

public sealed class TransferMoneyResponseDto
{
    public PaymentStatusEnum PaymentStatus { get; set; }
    public DateTime ProcessedAt { get; set; }
    public string SenderCardLast4 { get; set; } = string.Empty;
    public string ReceiverCardLast4 { get; set; } = string.Empty;
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public decimal Amount { get; set; }
}
