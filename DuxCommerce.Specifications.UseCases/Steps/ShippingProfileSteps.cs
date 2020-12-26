using DuxCommerce.Specifications.UseCases.Hooks;
using TechTalk.SpecFlow;

namespace DuxCommerce.Specifications.UseCases.Steps
{
    [Binding]
    public class ShippingProfileSteps
    {
        private readonly StepsContext _context;
        private readonly IApiClient _apiClient;

        public ShippingProfileSteps(StepsContext context, IApiClient apiClient)
        {
            _context = context;
            _apiClient = apiClient;
        }

        [Then(@"default shipping profile should be created as follow:")]
        public void ThenDefaultShippingProfileShouldBeCreatedAsFollow(Table table)
        {
        }

        [Then(@"shipping origin should be created as follow:")]
        public void ThenShippingOriginShouldBeCreatedAsFollow(Table table)
        {
        }

        [Then(@"shipping zone should be created as follow:")]
        public void ThenShippingZoneShouldBeCreatedAsFollow(Table table)
        {
        }

        [Then(@"shippig countries should be created as follow:")]
        public void ThenShippigCountriesShouldBeCreatedAsFollow(Table table)
        {
        }

        [Then(@"shippig states should be created as follow:")]
        public void ThenShippigStatesShouldBeCreatedAsFollow(Table table)
        {
        }
    }
}
