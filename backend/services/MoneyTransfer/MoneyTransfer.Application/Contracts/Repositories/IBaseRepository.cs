namespace ch4rniauski.BankApp.MoneyTransfer.Application.Contracts.Repositories;

public interface IBaseRepository<TEntity, in TId>
    where TEntity : class
    where TId : struct
{
    Task<bool> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
}