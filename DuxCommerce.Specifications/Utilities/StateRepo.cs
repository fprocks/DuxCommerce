using DuxCommerce.Common;
using DuxCommerce.Core.Shared.PublicTypes;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuxCommerce.Specifications.Utilities
{
    public class StateRepo
    {
        public static async Task<IEnumerable<StateDto>> GetAllStatesAsync()
        {
            var connection = MongoDatabase.GetConnection();
            var states = connection.GetCollection<StateDto>(CollectionName.State);
            return await states.Find(new BsonDocument()).ToListAsync();
        }
    }
}
