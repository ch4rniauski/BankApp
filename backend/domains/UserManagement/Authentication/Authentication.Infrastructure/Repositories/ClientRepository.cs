using ch4rniauski.BankApp.Authentication.Application.Contracts.Repositories;
using ch4rniauski.BankApp.Authentication.Domain.Entities;

namespace ch4rniauski.BankApp.Authentication.Infrastructure.Repositories;

public class ClientRepository : BaseRepository<ClientEntity, Guid>, IClientRepository
{
    public ClientRepository(AuthenticationContext context) : base(context)
    {
    }
}