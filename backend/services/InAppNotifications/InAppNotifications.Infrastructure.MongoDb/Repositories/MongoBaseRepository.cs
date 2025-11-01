using System.Linq.Expressions;
using ch4rniauski.BankApp.InAppNotifications.Application.Contracts.Repositories;
using ch4rniauski.BankApp.InAppNotifications.Application.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.Repositories;

public abstract class MongoBaseRepository<TEntity> : IMongoBaseRepository<TEntity> where TEntity : class
{
    protected readonly IMongoCollection<TEntity> Collection;
    
    protected MongoBaseRepository(MongoDbSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var db = client.GetDatabase(settings.DataBaseName);
        
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

    public async Task<IList<TEntity>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var filter = FilterDefinition<TEntity>.Empty;
        
        var cursor = await Collection.FindAsync(filter, cancellationToken: cancellationToken);
        
        return await cursor.ToListAsync(cancellationToken);
    }

    public async Task<IList<TProjection>?> GetAllAsync<TProjection>(TProjection projectionType, Expression<Func<TEntity, TProjection>> projectionExpression,
        CancellationToken cancellationToken = default)
    {
        var filter = FilterDefinition<TEntity>.Empty;
        var findOptions = new FindOptions<TEntity, TProjection>
        {
            Projection = Builders<TEntity>.Projection.Expression(projectionExpression)
        };
        
        var cursor = await Collection.FindAsync(filter, findOptions,  cancellationToken);
        
        return await cursor.ToListAsync(cancellationToken);
    }

    public async Task<bool> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var insertParams = new InsertOneOptions();

            await Collection.InsertOneAsync(entity, insertParams, cancellationToken);
            
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
