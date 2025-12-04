namespace ch4rniauski.BankApp.InAppNotifications.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAutoMapperConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(_ => {}, typeof(ServiceCollectionExtensions).Assembly);
    }
}
