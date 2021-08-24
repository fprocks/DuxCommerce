using System.Net.Http;
using BoDi;
using DuxCommerce.Specifications.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;
using WebApi;

namespace DuxCommerce.Specifications.Hooks
{
    [Binding]
    public class TestServerSetup
    {
        private static HttpClient _httpClient;
        private readonly IObjectContainer _objectContainer;

        public TestServerSetup(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            InitHttpWebServer();
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            RegisterDependencies();
        }

        private static void InitHttpWebServer()
        {
            var hostConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var hostBuilder = new WebHostBuilder()
                .UseConfiguration(hostConfig)
                .UseStartup<Startup>();

            var server = new TestServer(hostBuilder);
            _httpClient = server.CreateClient();
        }

        private void RegisterDependencies()
        {
            _objectContainer.RegisterInstanceAs(_httpClient);
            _objectContainer.RegisterTypeAs<ApiClient, IApiClient>();
        }
    }
}