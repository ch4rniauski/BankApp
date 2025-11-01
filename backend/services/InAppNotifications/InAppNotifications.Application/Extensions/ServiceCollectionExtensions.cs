using Microsoft.Extensions.DependencyInjection;

namespace ch4rniauski.BankApp.InAppNotifications.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMediatrConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
        });
    }

    public static void AddAutoMapperConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(_ => {}, typeof(ServiceCollectionExtensions).Assembly);
    }
}
