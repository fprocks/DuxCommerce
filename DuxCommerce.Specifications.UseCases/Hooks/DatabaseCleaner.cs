using Respawn;
using System.Configuration;

namespace DuxCommerce.Specifications.UseCases.Hooks
{
    public class DatabaseCleaner
    {
        public static void CleanUp()
        {
            var clientConnString = ConfigurationManager.ConnectionStrings["EmpLive.WebApi.Client"].ConnectionString;
            var clientCheckpoint = new Checkpoint()
            {
                TablesToIgnore = new[]
                {
                    "SchemaVersion",
                }
            };

            clientCheckpoint.Reset(clientConnString);
        }
    }
}
