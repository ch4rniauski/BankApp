using ch4rniauski.BankApp.CreditCards.Application.Contracts.Repositories;
using ch4rniauski.BankApp.CreditCards.Infrastructure.Repositories;
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
}
