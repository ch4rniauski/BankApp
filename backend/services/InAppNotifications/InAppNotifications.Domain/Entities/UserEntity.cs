using ch4rniauski.BankApp.InAppNotifications.Domain.Models;

namespace ch4rniauski.BankApp.InAppNotifications.Domain.Entities;

public class UserEntity
{
    public string Id { get; set; } = null!;
    
    public List<NotificationModel> Notifications { get; set; } = []; 
}
