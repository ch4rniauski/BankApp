using ch4rniauski.BankApp.InAppNotifications.Application.Contracts.Repositories;
using ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.Repositories;
using ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.Services.MongoDb;
using ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.Services.RabbitMQ;
using ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.Services.RabbitMQ.Consumers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMongoDataContextConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));

        services.AddScoped<IUsersRepository, UsersRepository>();
    }

    public static void AddRabbitMqConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMqSettings"));

        services.AddHostedService<NotificationConsumer>();
    }
}
