using System.Linq.Expressions;
using ch4rniauski.BankApp.InAppNotifications.Application.Contracts.Repositories;
using ch4rniauski.BankApp.InAppNotifications.Application.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.Repositories;

public abstract class MongoBaseRepository<TEntity> : IMongoBaseRepository<TEntity> where TEntity : class
{
    protected readonly IMongoCollection<TEntity> Collection;
    
    protected MongoBaseRepository(IOptions<MongoDbSettings> opt)
    {
        var mongoDbSettings = opt.Value;
        
        var client = new MongoClient(mongoDbSettings.ConnectionString);
        var db = client.GetDatabase(mongoDbSettings.DataBaseName);
        
        Collection = db.GetCollection<TEntity>(typeof(TEntity).Name);
    }

    public async Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<TEntity>.Filter.Eq("_id", id);
        var findOptions = new FindOptions<TEntity>
        {
            Limit = 1
        };
        
        var cursor = await Collection.FindAsync(filter, findOptions, cancellationToken);
        
        return await cursor.FirstOrDefaultAsync(cancellationToken);   
    }
    
    public async Task<TProjection?> GetByIdAsync<TProjection>(
        string id,
        TProjection projectionType,
        Expression<Func<TEntity, TProjection>> projectionExpression,
        CancellationToken cancellationToken = default)
    {
        var filter = Builders<TEntity>.Filter.Eq("_id", id);
        var findOptions = new FindOptions<TEntity, TProjection>
        {
            Limit = 1,
            Projection = Builders<TEntity>.Projection.Expression(projectionExpression)
        };
        
        var cursor = await Collection.FindAsync(filter, findOptions, cancellationToken);
        
        return await cursor.FirstOrDefaultAsync(cancellationToken);
    }
}
