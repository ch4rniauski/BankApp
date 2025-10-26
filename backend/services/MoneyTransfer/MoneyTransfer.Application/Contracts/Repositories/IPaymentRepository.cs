using ch4rniauski.BankApp.MoneyTransfer.Domain.Entities;

namespace ch4rniauski.BankApp.MoneyTransfer.Application.Contracts.Repositories;

public interface IPaymentRepository : IBaseRepository<PaymentEntity, Guid>
{
}
