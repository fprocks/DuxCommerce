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
            var shippingOrigins = mongodb.GetCollection<ShippingOriginDto>("ShippingOrigin");
            var shippingProfiles = mongodb.GetCollection<ShippingProfileDto>("ShippingProfile");

            // Todo: any better ways to delete all documents in a collection?
            storeProfile.DeleteMany(new BsonDocument());
            addresses.DeleteMany(new BsonDocument());
            shippingOrigins.DeleteMany(new BsonDocument());
            shippingProfiles.DeleteMany(new BsonDocument());
        }
    }
}
