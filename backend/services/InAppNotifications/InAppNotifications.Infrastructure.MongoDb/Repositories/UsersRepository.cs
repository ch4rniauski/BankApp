using ch4rniauski.BankApp.InAppNotifications.Application.Contracts.Repositories;
using ch4rniauski.BankApp.InAppNotifications.Application.MongoDb;
using ch4rniauski.BankApp.InAppNotifications.Domain.Entities;
using Microsoft.Extensions.Options;

namespace ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.Repositories;

public class UsersRepository : MongoBaseRepository<UserEntity>, IUsersRepository
{
    public UsersRepository(IOptions<MongoDbSettings> opt) : base(opt.Value)
    {
    }
}
