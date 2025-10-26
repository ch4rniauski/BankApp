using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ch4rniauski.BankApp.Authentication.Infrastructure.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task ApplyMigrations(this IApplicationBuilder app)
    {
        await using var scope = app.ApplicationServices.CreateAsyncScope();
        
        await using var db = scope.ServiceProvider.GetRequiredService<AuthenticationContext>();

        await db.Database.MigrateAsync();
    }
}
