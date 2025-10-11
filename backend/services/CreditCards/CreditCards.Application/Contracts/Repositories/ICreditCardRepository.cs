using ch4rniauski.BankApp.CreditCards.Domain.Entities;

namespace ch4rniauski.BankApp.CreditCards.Application.Contracts.Repositories;

public interface ICreditCardRepository : IBaseRepository<CreditCardEntity, Guid>
{
}
