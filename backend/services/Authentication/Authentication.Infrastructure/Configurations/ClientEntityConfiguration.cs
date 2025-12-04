using ch4rniauski.BankApp.Authentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ch4rniauski.BankApp.Authentication.Infrastructure.Configurations;

internal sealed class ClientEntityConfiguration : IEntityTypeConfiguration<ClientEntity>
{
    public void Configure(EntityTypeBuilder<ClientEntity> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder
            .Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        
        builder
            .Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder
            .Property(c => c.Email)
            .IsRequired();

        builder
            .Property(c => c.IsEmailVerified)
            .IsRequired();
        
        builder
            .Property(c => c.PhoneNumber)
            .IsRequired();
        
        builder
            .Property(c => c.PasswordHash)
            .IsRequired(false);
        
        builder
            .Property(c => c.RefreshToken)
            .IsRequired(false);
    }
}
