using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace DuxCommerce.Specifications.UseCases.Hooks
{
    [Binding]
    public sealed class DatabaseSetup
    {
        [BeforeScenario]
        public void BeforeScenario()
        {
            DatabaseCleaner.CleanUp();
        }
    }
}
