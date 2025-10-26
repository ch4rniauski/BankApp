using ch4rniauski.BankApp.CreditCards.Application.Contracts.HashProviders;
using ch4rniauski.BankApp.CreditCards.Application.Contracts.SensitiveDataProviders;
using ch4rniauski.BankApp.CreditCards.Application.Hash.Providers;
using ch4rniauski.BankApp.CreditCards.Application.Hash.Settings;
using ch4rniauski.BankApp.CreditCards.Application.SensitiveDataProviders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ch4rniauski.BankApp.CreditCards.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAutoMapperConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(_ => {}, typeof(ServiceCollectionExtensions).Assembly);
    }

    public static void AddSensitiveDataConfiguration(this IServiceCollection services)
    {
        services.AddScoped<ICvvProvider, CvvProvider>();
        services.AddScoped<IPinCodeProvider, PinCodeProvider>();
    }
    
    public static void AddHashConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<HmacSettings>(configuration.GetSection("HmacSettings"));
        
        services.AddScoped<IHashProvider, HmacHashProvider>();
    }
    
    public static void AddMediatrConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
        });
    }
}
