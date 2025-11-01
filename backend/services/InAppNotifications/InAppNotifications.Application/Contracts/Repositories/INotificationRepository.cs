using ch4rniauski.BankApp.InAppNotifications.Domain.Entities;

namespace ch4rniauski.BankApp.InAppNotifications.Application.Contracts.Repositories;

public interface INotificationRepository : IMongoBaseRepository<NotificationEntity>
{
}
