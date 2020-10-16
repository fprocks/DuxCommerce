using BoDi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using TechTalk.SpecFlow;

namespace DuxCommerce.Specifications.UseCases.Hooks
{
    [Binding]
    public class HostSetup
    {
        private readonly IObjectContainer _objectContainer;

        public HostSetup(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeTestRun]
        public void BeforeTestRun()
        {
            InitHttpWebServer();
        }

        private void InitHttpWebServer()
        {
            var hostConfig = new ConfigurationBuilder()
                .AddJsonFile("host-settings.json")
                .Build();

            var hostBuilder = new WebHostBuilder()
                .UseStartup<WebApi.Startup>();

            var server = new TestServer(hostBuilder);
            var client = server.CreateClient();

            _objectContainer.RegisterInstanceAs(client);
        }
    }
}
