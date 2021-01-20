using DuxCommerce.Common;
using MongoDB.Driver;

namespace DuxCommerce.Specifications.Utilities
{
    public class MongoDatabase
    {
        public static IMongoDatabase GetConnection()
        {
            var mongoSettings = MongoSettingsModule.FromFile("appsettings.json");
            var mongoClient = new MongoClient(mongoSettings.ConnectionString);
            return mongoClient.GetDatabase(mongoSettings.DatabaseName);
        }
    }
}
