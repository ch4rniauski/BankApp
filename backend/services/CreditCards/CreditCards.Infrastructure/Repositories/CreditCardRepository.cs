using ch4rniauski.BankApp.CreditCards.Application.Contracts.Repositories;
using ch4rniauski.BankApp.CreditCards.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ch4rniauski.BankApp.CreditCards.Infrastructure.Repositories;

public class CreditCardRepository : BaseRepository<CreditCardEntity, Guid>, ICreditCardRepository
{
    public CreditCardRepository(CreditCardsContext context) : base(context)
    {
    }

    public async Task<CreditCardEntity?> GetByCardNumberAsync(string cardNumber, CancellationToken cancellationToken = default)
        => await DbSet.FirstOrDefaultAsync(
            c => c.CardNumber == cardNumber,
            cancellationToken);

    public async Task<bool> TransferMoneyAsync(
        string senderCardNumber,
        string receiverCardNumber,
        decimal amount,
        CancellationToken cancellationToken = default)
    {
        await using var transaction = await Context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var senderCard = await DbSet
                .FromSql($"""
                        SELECT * FROM "CreditCards" AS c
                        WHERE c."CardNumber" = {senderCardNumber}
                        FOR UPDATE
                        """)
                .FirstAsync(cancellationToken);
            
            var receiverCard = await DbSet
                .FromSql($"""
                          SELECT * FROM "CreditCards" AS c
                          WHERE c."CardNumber" = {receiverCardNumber}
                          FOR UPDATE
                          """)
                .FirstAsync(cancellationToken);
            
            senderCard.Balance -= amount;
            receiverCard.Balance += amount;
            
            var changesCount = await Context.SaveChangesAsync(cancellationToken);
            
            await transaction.CommitAsync(cancellationToken);
            
            return changesCount > 0;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            
            return false;
        }
    }
}
