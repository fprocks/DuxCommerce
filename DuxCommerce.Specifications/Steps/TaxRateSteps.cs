using DuxCommerce.Specifications.UseCases.Hooks;
using TechTalk.SpecFlow;

namespace DuxCommerce.Specifications.Steps
{
    [Binding]
    public class TaxRateSteps
    {
        private readonly StepsContext _context;
        private readonly IApiClient _apiClient;

        public TaxRateSteps(StepsContext context, IApiClient apiClient)
        {
            _context = context;
            _apiClient = apiClient;
        }

        [Given(@"Tom selects country (.*)")]
        public void GivenTomSelectsCountry(string p0)
        {
        }
        
        [Given(@"Tome selects state (.*)")]
        public void GivenTomeSelectsState(string p0)
        {
        }
        
        [Given(@"Tom enters tax rate (.*)")]
        public void GivenTomEntersTaxRate(string p0)
        {
        }
        
        [When(@"Tom saves the tax rate")]
        public void WhenTomSavesTheTaxRate()
        {
        }
        
        [Then(@"Tom should receive status code OK")]
        public void ThenTomShouldReceiveStatusCodeOK()
        {
        }
        
        [Then(@"Tax rate should be created as expected")]
        public void ThenTaxRateShouldBeCreatedAsExpected()
        {
        }
    }
}
