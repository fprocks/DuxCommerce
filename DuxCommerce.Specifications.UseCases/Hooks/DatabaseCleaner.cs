using Respawn;
using System.Configuration;
using System.Data.SqlClient;

namespace DuxCommerce.Specifications.UseCases.Hooks
{
    public class DatabaseCleaner
    {
        public static void CleanUp()
        {
            var connString = "Server=(local);Database=DuxCommerce;User Id=DuxAdmin;Password=Password1;";
            //var connString = ConfigurationManager.ConnectionStrings["DuxCommerce"].ConnectionString;

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
