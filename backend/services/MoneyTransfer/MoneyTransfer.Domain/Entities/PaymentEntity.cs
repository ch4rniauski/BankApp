using ch4rniauski.BankApp.MoneyTransfer.Domain.Enums;

namespace ch4rniauski.BankApp.MoneyTransfer.Domain.Entities;

public class PaymentEntity
{
    public Guid Id { get; set; }

    public string Currency { get; set; } = string.Empty;
    
    public decimal Amount { get; set; }
    
    public string SenderCardLast4 { get; set; } = string.Empty;
    
    public string ReceiverCardLast4 { get; set; } = string.Empty;

    public Guid SenderId { get; set; }
    
    public Guid ReceiverId { get; set; }

    public string? Description { get; set; }
    
    public PaymentStatusEnum Status { get; set; } = PaymentStatusEnum.Pending;

    public DateTime ProcessedAt { get; set; }
}
