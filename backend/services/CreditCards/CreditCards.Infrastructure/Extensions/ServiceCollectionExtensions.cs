using ch4rniauski.BankApp.CreditCards.Application.Contracts.RabbitMQ;
using ch4rniauski.BankApp.CreditCards.Application.Contracts.Repositories;
using ch4rniauski.BankApp.CreditCards.Infrastructure.Repositories;
using ch4rniauski.BankApp.CreditCards.Infrastructure.Services.RabbitMQ;
using ch4rniauski.BankApp.CreditCards.Infrastructure.Services.RabbitMQ.Producers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ch4rniauski.BankApp.CreditCards.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCreditCardContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CreditCardsContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("PostgresCreditCards"));
        });

        services.AddScoped<ICreditCardRepository, CreditCardRepository>();
    }
    
    public static void AddRabbitMqConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMqSettings"));

        services.AddScoped<INotificationProducer, NotificationProducer>();
    }
}
