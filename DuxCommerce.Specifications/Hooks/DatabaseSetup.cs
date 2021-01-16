using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace DuxCommerce.Specifications.UseCases.Hooks
{
    [Binding]
    public sealed class DatabaseSetup
    {
        [BeforeTestRun]
        public static async Task BeforeScenarioAsync()
        {
            await MongoSetup.ResetAsync();
            //await MongoSetup.InitAsync();
        }
    }
}
