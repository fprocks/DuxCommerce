using DuxCommerce.Common;
using Respawn;
using System.Data.SqlClient;

namespace DuxCommerce.Specifications.UseCases.Hooks
{
    public class DatabaseCleaner
    {
        public static void CleanUp()
        {
            var dbSettings = DatabaseSettingsModule.FromFile("appsettings.json");
            var connection = new SqlConnection(dbSettings.ConnectionString);

            connection.Open();

            var clientCheckpoint = new Checkpoint()
            {
                TablesToIgnore = new[]
                {
                    "VersionInfo",
                    "Country",
                    "State"
                }
            };

            clientCheckpoint.Reset(connection);
        }
    }
}
