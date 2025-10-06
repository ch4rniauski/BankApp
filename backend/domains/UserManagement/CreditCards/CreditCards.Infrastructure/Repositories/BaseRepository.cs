using ch4rniauski.BankApp.CreditCards.Application.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ch4rniauski.BankApp.CreditCards.Infrastructure.Repositories;

public abstract class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId>
    where TEntity : class
    where TId : struct
{
    protected readonly CreditCardsContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(CreditCardsContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }
    
    public async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
        => await _dbSet.FindAsync(
            keyValues: [id],
            cancellationToken: cancellationToken);

    public async Task<bool> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
