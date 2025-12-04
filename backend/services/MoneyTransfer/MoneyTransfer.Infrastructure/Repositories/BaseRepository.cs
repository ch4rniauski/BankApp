using ch4rniauski.BankApp.MoneyTransfer.Application.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ch4rniauski.BankApp.MoneyTransfer.Infrastructure.Repositories;

internal abstract class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId>
    where TEntity : class
    where TId : struct
{
    protected readonly MoneyTransferContext Context;
    protected readonly DbSet<TEntity> DbSet;

    protected BaseRepository(MoneyTransferContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }
    
    public async Task<bool> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        
        return await Context.SaveChangesAsync(cancellationToken) > 0;
    }
}
