using DuxCommerce.Core.Taxes.PublicTypes;
using DuxCommerce.Specifications.UseCases.Hooks;
using TechTalk.SpecFlow;

namespace DuxCommerce.Specifications.Steps
{
    [Binding]
    public class TaxRateSteps
    {
        private readonly StepsContext _context;
        private readonly IApiClient _apiClient;

        private TaxRateDto _taxRateDto;

        public TaxRateSteps(StepsContext context, IApiClient apiClient)
        {
            _context = context;
            _apiClient = apiClient;
        }

        [Given(@"Tom enters tax rate name GST")]
        public void GivenTomEntersTaxRateNameGST()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"Tom selects the following countries:")]
        public void GivenTomSelectsTheFollowingCountries(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"Tom selects the following states:")]
        public void GivenTomSelectsTheFollowingStates(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"Tom enters the following postal codes:")]
        public void GivenTomEntersTheFollowingPostalCodes(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"Tom enters postal codes (.*)")]
        public void GivenTomEntersPostalCodes(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"Tom enters tax rate amount (.*)")]
        public void GivenTomEntersTaxRateAmount(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"Tom saves the tax rate")]
        public void WhenTomSavesTheTaxRate()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"Tax rate should be created as expected")]
        public void ThenTaxRateShouldBeCreatedAsExpected()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
