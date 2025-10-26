using ch4rniauski.BankApp.Authentication.Application.Contracts.Repositories;
using ch4rniauski.BankApp.Authentication.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ch4rniauski.BankApp.Authentication.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAuthenticationContextConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuthenticationContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("PostgresAuthentication"));
        });

        services.AddScoped<IClientRepository, ClientRepository>();
    }
}
