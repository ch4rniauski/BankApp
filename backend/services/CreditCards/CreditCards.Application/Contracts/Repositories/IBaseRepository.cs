namespace ch4rniauski.BankApp.CreditCards.Application.Contracts.Repositories;

public interface IBaseRepository<TEntity, in TId>
    where TEntity : class
    where TId : struct
{
    Task<bool> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
}
