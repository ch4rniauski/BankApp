using System.Linq.Expressions;
using ch4rniauski.BankApp.Authentication.Application.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ch4rniauski.BankApp.Authentication.Infrastructure.Repositories;

public abstract class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId>
    where TEntity : class
    where TId : struct
{
    protected readonly AuthenticationContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(AuthenticationContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }
    
    public async Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbSet
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
        => await _dbSet.FindAsync(
            keyValues: [id],
            cancellationToken: cancellationToken);

    public async Task<bool> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteWithCriteriaAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken = default)
        => await _dbSet
            .Where(criteria)
            .ExecuteDeleteAsync(cancellationToken) > 0;

    public async Task<bool> DeleteWithAttachmentAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);
        
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
