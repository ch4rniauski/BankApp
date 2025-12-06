using ch4rniauski.BankApp.MoneyTransfer.Application.Contracts.Repositories;
using ch4rniauski.BankApp.MoneyTransfer.Domain.Entities;

namespace ch4rniauski.BankApp.MoneyTransfer.Infrastructure.Repositories;

internal class PaymentRepository : BaseRepository<PaymentEntity, Guid>, IPaymentRepository
{
    public PaymentRepository(MoneyTransferContext context) : base(context)
    {
    }
}
