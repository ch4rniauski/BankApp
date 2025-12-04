using ch4rniauski.BankApp.InAppNotifications.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.BsonClassMaps.Maps;

internal static class UserEntityMap
{
    public static void Register()
    {
        BsonClassMap.RegisterClassMap<UserEntity>(cm =>
        {
            cm.AutoMap();
            
            cm
                .MapIdProperty(n => n.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.String));
        });
    }
}
