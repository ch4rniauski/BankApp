using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ch4rniauski.BankApp.Authentication.Infrastructure.Extensions.DependencyInjectionExtensions;

public static class ServiceCollectionExtensions
{
    public static void AddAuthenticationContextConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuthenticationContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("PostgresAuthentication"));
        });
    }
}