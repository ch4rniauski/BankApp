using ch4rniauski.BankApp.CreditCards.Application.Contracts.Repositories;
using ch4rniauski.BankApp.CreditCards.Domain.Entities;

namespace ch4rniauski.BankApp.CreditCards.Infrastructure.Repositories;

public class CreditCardRepository : BaseRepository<CreditCardEntity, Guid>, ICreditCardRepository
{
    public CreditCardRepository(CreditCardsContext context) : base(context)
    {
    }   
}
