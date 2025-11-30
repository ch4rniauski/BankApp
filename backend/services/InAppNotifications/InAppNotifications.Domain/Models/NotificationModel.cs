namespace ch4rniauski.BankApp.InAppNotifications.Domain.Models;

public class NotificationModel
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public bool IsRead { get; set; }
}
