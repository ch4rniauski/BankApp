namespace ch4rniauski.BankApp.CreditCards.Domain.Entities;

public class CreditCardEntity
{
    public Guid Id { get; set; }

    public string CardNumber { get; set; } = null!;
    
    public string CvvHash { get; set; } = null!;
    
    public string CardType { get; set; } = null!;
    
    public byte ExpirationMonth { get; set; }
    
    public short ExpirationYear { get; set; }
    
    public bool IsBlocked { get; set; }
    
    public string CardHolderName { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
    
    public Guid CardHolderId { get; set; }
}
