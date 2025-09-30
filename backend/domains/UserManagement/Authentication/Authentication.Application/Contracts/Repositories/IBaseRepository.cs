using System.Linq.Expressions;

namespace ch4rniauski.BankApp.Authentication.Application.Contracts.Repositories;

public interface IBaseRepository<TEntity, in TId> 
    where TEntity : class
    where TId : struct
{
    Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
    Task<bool> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteWithCriteriaAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken = default);
    Task<bool> DeleteWithAttachmentAsync(TEntity entity, CancellationToken cancellationToken = default);
}