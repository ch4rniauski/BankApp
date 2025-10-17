using ch4rniauski.BankApp.MoneyTransfer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ch4rniauski.BankApp.MoneyTransfer.Infrastructure;

public class MoneyTransferContext : DbContext
{
    public DbSet<PaymentEntity> Payments { get; set; }
    
    public MoneyTransferContext()
    {
    }

    public MoneyTransferContext(DbContextOptions<MoneyTransferContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MoneyTransferContext).Assembly);
    }
}
