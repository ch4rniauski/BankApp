using ch4rniauski.BankApp.Authentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ch4rniauski.BankApp.Authentication.Infrastructure;

public class AuthenticationContext : DbContext
{
    public DbSet<ClientEntity> Clients { get; set; }

    public AuthenticationContext()
    {
    }
    
    public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthenticationContext).Assembly);
    }
}
