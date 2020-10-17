using BoDi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using TechTalk.SpecFlow;

namespace DuxCommerce.Specifications.UseCases.Hooks
{
    [Binding]
    public class HostSetup
    {
        private readonly IObjectContainer _objectContainer;
        private static HttpClient _httpClient;

        public HostSetup(IObjectContainer objectContainer)
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
                .AddJsonFile("host-settings.json")
                .Build();

            var hostBuilder = new WebHostBuilder()
                .UseStartup<WebApi.Startup>();

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
