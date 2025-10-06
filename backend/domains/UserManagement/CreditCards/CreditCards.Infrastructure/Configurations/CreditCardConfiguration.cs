using ch4rniauski.BankApp.CreditCards.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ch4rniauski.BankApp.CreditCards.Infrastructure.Configurations;

public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCardEntity>
{
    public void Configure(EntityTypeBuilder<CreditCardEntity> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder
            .Property(c => c.CardNumber)
            .HasMaxLength(19)
            .IsRequired();

        builder
            .Property(c => c.CvvHash)
            .IsRequired();

        builder
            .Property(c => c.CardType)
            .HasMaxLength(20)
            .IsRequired();
        
        builder
            .Property(c => c.ExpirationMonth)
            .IsRequired();
        
        builder
            .Property(c => c.ExpirationYear)
            .IsRequired();
        
        builder
            .Property(c => c.IsBlocked)
            .IsRequired();
        
        builder
            .Property(c => c.CardHolderName)
            .HasMaxLength(150)
            .IsRequired();
        
        builder
            .Property(c => c.CreatedAt)
            .IsRequired();
        
        builder
            .Property(c => c.CardHolderId)
            .IsRequired();
    }
}
