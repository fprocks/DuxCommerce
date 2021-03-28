using DuxCommerce.Core.Checkout.PublicTypes;
using DuxCommerce.Core.Shared.PublicTypes;
using DuxCommerce.Specifications.UseCases.Hooks;
using DuxCommerce.Specifications.Utilities;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DuxCommerce.Specifications.Steps
{
    [Binding]
    public class CreateShippingAddressSteps
    {
        private readonly StepContext _context;
        private readonly IApiClient _apiClient;

        private CheckoutDto _checkoutRequest = new CheckoutDto();

        public CreateShippingAddressSteps(StepContext context, IApiClient apiClient)
        {
            _context = context;
            _apiClient = apiClient;
        }

        [Given(@"Amy enters the email address (.*)")]
        public void GivenAmyEntersTheEmailAddress(string email)
        {
            _checkoutRequest.Email = email;
        }
        
        [Given(@"Amy enters the following shipping address")]
        public void GivenAmyEntersTheFollowingShippingAddress(Table table)
        {
            var address = table.CreateSet<AddressDto>().FirstOrDefault();
            _checkoutRequest.ShippingAddress = address;
        }
        
        [When(@"Amy saves the shipping address")]
        public async System.Threading.Tasks.Task WhenAmySavesTheShippingAddressAsync()
        {
            var url = $"api/checkout/{_context.ShopperId}/shippingaddress";
            var apiResult = await _apiClient.PostAsync(url, _checkoutRequest);
            _context.ApiResult = apiResult;
        }
        
        [Then(@"the shipping address should be created as expected")]
        public void ThenTheShippingAddressShouldBeCreatedAsExpected()
        {
        }
    }
}
