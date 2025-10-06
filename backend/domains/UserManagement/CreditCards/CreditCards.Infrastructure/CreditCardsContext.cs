using ch4rniauski.BankApp.CreditCards.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ch4rniauski.BankApp.CreditCards.Infrastructure;

public class CreditCardsContext : DbContext
{
    public DbSet<CreditCardEntity> CreditCards { get; set; }

    public CreditCardsContext()
    {
    }

    public CreditCardsContext(DbContextOptions<CreditCardsContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CreditCardsContext).Assembly);
    }
}
