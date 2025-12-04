using ch4rniauski.BankApp.InAppNotifications.Domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.BsonClassMaps.Maps;

internal static class NotificationModelMap
{
    public static void Register()
    {
        BsonClassMap.RegisterClassMap<NotificationModel>(cm =>
        {
            cm.AutoMap();

            cm
                .MapIdProperty(n => n.Id)
                .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
        });
    }
}
