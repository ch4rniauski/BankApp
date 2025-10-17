using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ch4rniauski.BankApp.MoneyTransfer.Infrastructure.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddMoneyTransferConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MoneyTransferContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("PostgresMoneyTransfer"));
        });
    }
}
