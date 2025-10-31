using ch4rniauski.BankApp.InAppNotifications.Application.MongoDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMongoDataContextConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
    }
}
