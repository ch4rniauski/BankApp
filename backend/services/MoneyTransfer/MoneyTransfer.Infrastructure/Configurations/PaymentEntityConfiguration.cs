using ch4rniauski.BankApp.MoneyTransfer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ch4rniauski.BankApp.MoneyTransfer.Infrastructure.Configurations;

internal sealed class PaymentEntityConfiguration : IEntityTypeConfiguration<PaymentEntity>
{
    public void Configure(EntityTypeBuilder<PaymentEntity> builder)
    {
        builder
            .HasKey(p => p.Id);

        builder
            .Property(p => p.Currency)
            .HasMaxLength(3)
            .IsRequired();

        builder
            .Property(p => p.Amount)
            .IsRequired();
        
        builder
            .Property(p => p.SenderCardLast4)
            .IsFixedLength()
            .HasMaxLength(4)
            .IsRequired();
        
        builder
            .Property(p => p.ReceiverCardLast4)
            .IsFixedLength()
            .HasMaxLength(4)
            .IsRequired();

        builder
            .Property(p => p.SenderId)
            .IsRequired();
        
        builder
            .Property(p => p.ReceiverId)
            .IsRequired();
        
        builder
            .Property(p => p.Description)
            .HasMaxLength(150)
            .IsRequired(false);
        
        builder
            .Property(p => p.Status)
            .HasConversion<string>()
            .HasMaxLength(15)
            .IsRequired();
        
        builder
            .Property(p => p.ProcessedAt)
            .IsRequired();

        builder
            .ToTable(t => t.HasCheckConstraint(
                "CK_Payments_Amount_Positive",
                "\"Amount\" >= 0"));
    }
}
