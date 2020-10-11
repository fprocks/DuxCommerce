using Respawn;
using System.Configuration;
using System.Data.SqlClient;

namespace DuxCommerce.Specifications.UseCases.Hooks
{
    public class DatabaseCleaner
    {
        public static void CleanUp()
        {
            var connString = "data source = (local); initial catalog = DuxCommerce; user id = sa; password = p@ssw0rd";

            var connection = new SqlConnection(connString);
            connection.Open();

            var clientCheckpoint = new Checkpoint()
            {
                TablesToIgnore = new[]
                {
                    "VersionInfo",
                }
            };

            clientCheckpoint.Reset(connection);
        }
    }
}
