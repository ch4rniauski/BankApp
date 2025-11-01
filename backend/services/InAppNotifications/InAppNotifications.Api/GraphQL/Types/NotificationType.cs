namespace ch4rniauski.BankApp.InAppNotifications.Api.GraphQL.Types;

public class NotificationType
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public bool IsRead { get; set; }
}
