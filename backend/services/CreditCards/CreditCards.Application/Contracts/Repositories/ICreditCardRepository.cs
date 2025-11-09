using ch4rniauski.BankApp.CreditCards.Domain.Entities;

namespace ch4rniauski.BankApp.CreditCards.Application.Contracts.Repositories;

public interface ICreditCardRepository : IBaseRepository<CreditCardEntity, Guid>
{
    Task<CreditCardEntity?> GetCardByNumberAsync(string cardNumber, CancellationToken cancellationToken = default);

    Task<IList<TMap>> GetCardsByClientId<TMap>(Guid clientId, CancellationToken cancellationToken = default);
    
    Task<bool> TransferMoneyAsync(
        string senderCardNumber,
        string receiverCardNumber,
        decimal amount,
        CancellationToken cancellationToken = default);
}
