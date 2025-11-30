namespace ch4rniauski.BankApp.InAppNotifications.Domain.Messages;

public class NotificationMessage
{
    public Guid UserId { get; set; }
    
    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;
}
