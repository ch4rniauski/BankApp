using System.Linq.Expressions;

namespace ch4rniauski.BankApp.InAppNotifications.Application.Contracts.Repositories;

public interface IMongoBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    
    Task<TProjection?> GetByIdAsync<TProjection>(
        string id,
        Expression<Func<TEntity, TProjection>> projectionExpression,
        CancellationToken cancellationToken = default);
    
    Task<IList<TEntity>?> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<IList<TProjection>?> GetAllAsync<TProjection>(
        Expression<Func<TEntity, TProjection>> projectionExpression,
        CancellationToken cancellationToken = default);
    
    Task<bool> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    Task<bool> UpdateAsync(
        string id,
        TEntity entity,
        CancellationToken cancellationToken = default);
    
    Task<TProjection?> UpdateAsync<TProjection>(
        string id,
        TEntity entity,
        Expression<Func<TEntity, TProjection>> projectionExpression,
        CancellationToken cancellationToken = default);
}
