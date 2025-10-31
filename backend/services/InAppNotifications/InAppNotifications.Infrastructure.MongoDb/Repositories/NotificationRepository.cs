using ch4rniauski.BankApp.InAppNotifications.Application.Contracts.Repositories;
using ch4rniauski.BankApp.InAppNotifications.Domain.Entities;

namespace ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.Repositories;

public class NotificationRepository : INotificationRepository
{
    public async Task<NotificationEntity?> GetByIdAsync(string key, string id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
