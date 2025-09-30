using ch4rniauski.BankApp.Authentication.Domain.Entities;

namespace ch4rniauski.BankApp.Authentication.Application.Contracts.Repositories;

public interface IClientRepository : IBaseRepository<ClientEntity, Guid>
{
}