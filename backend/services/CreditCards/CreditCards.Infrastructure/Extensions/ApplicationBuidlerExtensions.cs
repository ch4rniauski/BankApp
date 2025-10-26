using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ch4rniauski.BankApp.CreditCards.Infrastructure.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task ApplyMigrations(this IApplicationBuilder app)
    {
        await using var scope = app.ApplicationServices.CreateAsyncScope();
        
        await using var db = scope.ServiceProvider.GetRequiredService<CreditCardsContext>();

        await db.Database.MigrateAsync();
    }
}
