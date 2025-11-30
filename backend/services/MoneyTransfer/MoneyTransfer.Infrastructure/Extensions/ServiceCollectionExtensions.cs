using ch4rniauski.BankApp.MoneyTransfer.Application.Contracts.Repositories;
using ch4rniauski.BankApp.MoneyTransfer.Infrastructure.Repositories;
using ch4rniauski.BankApp.MoneyTransfer.Infrastructure.Services.RabbitMQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ch4rniauski.BankApp.MoneyTransfer.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMoneyTransferConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MoneyTransferContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("PostgresMoneyTransfer"));
        });

        services.AddScoped<IPaymentRepository, PaymentRepository>();
    }

    public static void AddRabbitMqConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMqSettings"));
    }
}
