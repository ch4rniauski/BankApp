using ch4rniauski.BankApp.CreditCards.Domain.Entities;

namespace ch4rniauski.BankApp.CreditCards.Application.Contracts.Repositories;

public interface ICreditCardRepository : IBaseRepository<CreditCardEntity, Guid>
{
    Task<CreditCardEntity?> GetByCardNumberAsync(string cardNumber, CancellationToken cancellationToken = default);
    
    Task<bool> TransferMoneyAsync(
        string senderCardNumber,
        string receiverCardNumber,
        decimal amount,
        CancellationToken cancellationToken = default);
}
