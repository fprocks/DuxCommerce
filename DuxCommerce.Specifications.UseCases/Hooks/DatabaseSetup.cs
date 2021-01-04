using TechTalk.SpecFlow;

namespace DuxCommerce.Specifications.UseCases.Hooks
{
    [Binding]
    public sealed class DatabaseSetup
    {
        [BeforeTestRun]
        public static void BeforeScenario()
        {
            SqlDatabaseCleaner.CleanUp();
            MongoDatabaseCleaner.CleanUpAsync();
        }
    }
}
