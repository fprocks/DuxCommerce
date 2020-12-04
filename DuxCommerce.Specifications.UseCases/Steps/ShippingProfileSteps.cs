using DuxCommerce.Specifications.UseCases.Hooks;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace DuxCommerce.Specifications.UseCases.Steps
{
    [Binding]
    public class ShippingProfileSteps
    {
        private readonly Hooks.ScenarioContext _context;
        private readonly IApiClient _apiClient;

        public ShippingProfileSteps(Hooks.ScenarioContext context, IApiClient apiClient)
        {
            _context = context;
            _apiClient = apiClient;
        }

        [Then(@"the general shipping profile should be created")]
        public void ThenTheGeneralShippingProfileShouldBeCreated()
        {
        }
    }
}
