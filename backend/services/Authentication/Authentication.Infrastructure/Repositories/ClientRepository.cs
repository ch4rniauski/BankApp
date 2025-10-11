using ch4rniauski.BankApp.Authentication.Application.Contracts.Repositories;
using ch4rniauski.BankApp.Authentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ch4rniauski.BankApp.Authentication.Infrastructure.Repositories;

public class ClientRepository : BaseRepository<ClientEntity, Guid>, IClientRepository
{
    public ClientRepository(AuthenticationContext context) : base(context)
    {
    }

    public async Task<ClientEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken = default) 
        => await _dbSet.FirstOrDefaultAsync(c => c.Email == email, cancellationToken);
}
