using ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.BsonClassMaps.Maps;

namespace ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.BsonClassMaps;

public static class BsonClassMapRegistry
{
    public static void RegisterBsonClassMaps()
    {
        UserEntityMap.Register();
    }
}
