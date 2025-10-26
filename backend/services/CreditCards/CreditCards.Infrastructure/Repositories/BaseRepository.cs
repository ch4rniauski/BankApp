using ch4rniauski.BankApp.CreditCards.Application.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ch4rniauski.BankApp.CreditCards.Infrastructure.Repositories;

public abstract class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId>
    where TEntity : class
    where TId : struct
{
    protected readonly CreditCardsContext Context;
    protected readonly DbSet<TEntity> DbSet;

    protected BaseRepository(CreditCardsContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }
    
    public async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
        => await DbSet.FindAsync(
            keyValues: [id],
            cancellationToken: cancellationToken);

    public async Task<bool> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        
        return await Context.SaveChangesAsync(cancellationToken) > 0;
    }
}
