using ch4rniauski.BankApp.InAppNotifications.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.BsonClassMaps.Maps;

public static class NotificationEntityMap
{
    public static void Register()
    {
        BsonClassMap.RegisterClassMap<NotificationEntity>(cm =>
        {
            cm.AutoMap();
            
            cm
                .MapIdProperty(n => n.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
        });
    }    
}
