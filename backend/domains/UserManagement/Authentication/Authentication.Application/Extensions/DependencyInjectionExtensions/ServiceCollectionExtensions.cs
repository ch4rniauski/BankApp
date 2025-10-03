using System.Text;
using ch4rniauski.BankApp.Authentication.Application.Contracts.Jwt;
using ch4rniauski.BankApp.Authentication.Application.Jwt;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ch4rniauski.BankApp.Authentication.Application.Extensions.DependencyInjectionExtensions;

public static class ServiceCollectionExtensions
{
    public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecurityKey"]!)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
        
        services.AddAuthorization();

        services.AddScoped<ITokenProvider, JwtTokenProvider>();
        
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
    }

    public static void AddValidationConfiguration(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
    }

    public static void AddAutoMapperConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => {}, typeof(ServiceCollectionExtensions).Assembly);
    }
}
