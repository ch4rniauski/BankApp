using ch4rniauski.BankApp.InAppNotifications.Application.Contracts.Repositories;
using ch4rniauski.BankApp.InAppNotifications.Application.MongoDb;
using ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMongoDataContextConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));

        services.AddScoped<INotificationRepository, NotificationRepository>();
    }
}
