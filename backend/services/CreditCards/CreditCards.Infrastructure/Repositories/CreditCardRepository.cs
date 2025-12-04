using AutoMapper;
using AutoMapper.QueryableExtensions;
using ch4rniauski.BankApp.CreditCards.Application.Contracts.Repositories;
using ch4rniauski.BankApp.CreditCards.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ch4rniauski.BankApp.CreditCards.Infrastructure.Repositories;

internal class CreditCardRepository : BaseRepository<CreditCardEntity, Guid>, ICreditCardRepository
{
    public CreditCardRepository(
        CreditCardsContext context,
        IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<CreditCardEntity?> GetCardByNumberAsync(string cardNumber, CancellationToken cancellationToken = default)
        => await DbSet.FirstOrDefaultAsync(
            c => c.CardNumber == cardNumber,
            cancellationToken);
    
    public async Task<IList<TMap>> GetCardsByClientId<TMap>(Guid clientId, CancellationToken cancellationToken = default)
        => await DbSet
            .Where(c => c.CardHolderId == clientId)
            .AsNoTracking()
            .ProjectTo<TMap>(Mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

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
