using System.Threading.Tasks;
using DuxCommerce.Specifications.Utilities;
using TechTalk.SpecFlow;

namespace DuxCommerce.Specifications.Hooks
{
    [Binding]
    public sealed class DatabaseSetup
    {
        [BeforeTestRun]
        public static async Task BeforeScenarioAsync()
        {
            await MongoDbSetup.ResetAsync();
            await MongoDbSetup.InitAsync();
        }
    }
}