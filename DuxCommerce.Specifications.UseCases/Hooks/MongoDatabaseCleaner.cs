using DuxCommerce.Common;
using DuxCommerce.Settings.PublicTypes;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DuxCommerce.Specifications.UseCases.Hooks
{
    public class MongoDatabaseCleaner
    {
        public static void CleanUpAsync()
        {
            var mongoSettings = MongoSettingsModule.FromFile("appsettings.json");

            var mongoClient = new MongoClient(mongoSettings.ConnectionString);
            var mongodb = mongoClient.GetDatabase(mongoSettings.DatabaseName);

            var storeProfile = mongodb.GetCollection<StoreProfileDto>("StoreProfile");
            var addresses = mongodb.GetCollection<AddressDto>("Address");

            // Todo: any better ways to delete all documents in a collection?
            storeProfile.DeleteMany(new BsonDocument());
            addresses.DeleteMany(new BsonDocument());
        }
    }
}
