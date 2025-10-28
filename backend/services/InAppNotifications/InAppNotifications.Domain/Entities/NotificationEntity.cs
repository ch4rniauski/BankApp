namespace ch4rniauski.BankApp.InAppNotifications.Domain.Entities;

public class NotificationEntity
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public bool IsRead { get; set; }
}
