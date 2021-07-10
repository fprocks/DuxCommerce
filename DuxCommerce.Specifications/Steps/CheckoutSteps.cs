using DuxCommerce.Core.Checkout.PublicTypes;
using DuxCommerce.Core.Shared.PublicTypes;
using DuxCommerce.Specifications.UseCases.Hooks;
using DuxCommerce.Specifications.Utilities;
using FluentAssertions;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DuxCommerce.Specifications.Steps
{
    [Binding]
    public class CreateShippingAddressSteps
    {
        private readonly StepContext _context;
        private readonly IApiClient _apiClient;

        private CustomerInfoRequest _request = new CustomerInfoRequest();

        public CreateShippingAddressSteps(StepContext context, IApiClient apiClient)
        {
            _context = context;
            _apiClient = apiClient;
        }

        [When(@"Amy enters the email address (.*)")]
        [Given(@"Amy enters the email address (.*)")]
        public void GivenAmyEntersTheEmailAddress(string email)
        {
            _request.Email = email;
        }
        
        [Given(@"Amy enters the following shipping address:")]
        public void GivenAmyEntersTheFollowingShippingAddress(Table table)
        {
            var address = table.CreateSet<AddressDto>().FirstOrDefault();
            _request.ShippingAddress = address;
        }

        [When(@"Amy saves the contact details and shipping address")]
        [Given(@"Amy saves the contact details and shipping address")]
        public async Task WhenAmySavesTheContactDetailsAndShippingAddressAsync()
        {
            var url = $"api/checkout/{_context.ShopperId}/customerinfo";
            var apiResult = await _apiClient.PostAsync(url, _request);
            _context.ApiResult = apiResult;
        }

        [Given(@"Amy selects shipping method (.*)")]
        public void GivenAmySelectsShippingMethod(int shippingMethod)
        {
            
        }

        [When(@"Amy saves the shipping method")]
        public void WhenAmySavesTheShippingMethod()
        {
            
        }

        [Then(@"checkout information should be saved as expected")]
        public async Task ThenCheckoutInformationShouldBeSavedAsExpectedAsync()
        {
            var checkout = await GetCheckout(_context.ApiResult);
            _request.Email.Should().Be(checkout.Email);
            _request.ShippingAddress.Equals(checkout.ShippingAddress);
        }

        private async Task<CheckoutDto> GetCheckout(HttpResponseMessage apiResult)
		{
            var resultString = await apiResult.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CheckoutDto>(resultString);
        }
	}
}
