using System.Linq.Expressions;
using ch4rniauski.BankApp.Authentication.Application.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ch4rniauski.BankApp.Authentication.Infrastructure.Repositories;

internal abstract class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId>
    where TEntity : class
    where TId : struct
{
    protected readonly AuthenticationContext Context;
    protected readonly DbSet<TEntity> DbSet;

    protected BaseRepository(AuthenticationContext context)
    {
        Context = context;
        DbSet = Context.Set<TEntity>();
    }
    
    public async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
        => await DbSet.FindAsync(
            keyValues: [id],
            cancellationToken: cancellationToken);

    public async Task<bool> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);

        return await Context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteWithCriteriaAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken = default)
        => await DbSet
            .Where(criteria)
            .ExecuteDeleteAsync(cancellationToken) > 0;

    public async Task<bool> DeleteWithAttachmentAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Remove(entity);
        
        return await Context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Update(entity);
        
        return await Context.SaveChangesAsync(cancellationToken) > 0;
    }
}
