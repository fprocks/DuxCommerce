using DuxCommerce.Core.Checkout.PublicTypes;
using DuxCommerce.Core.Shared.PublicTypes;
using DuxCommerce.Specifications.UseCases.Hooks;
using DuxCommerce.Specifications.Utilities;
using System.Linq;
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

        private CustomerInfoRequest _customerInfo = new CustomerInfoRequest();

        public CreateShippingAddressSteps(StepContext context, IApiClient apiClient)
        {
            _context = context;
            _apiClient = apiClient;
        }

        [Given(@"Amy enters the email address (.*)")]
        public void GivenAmyEntersTheEmailAddress(string email)
        {
            _customerInfo.Email = email;
        }
        
        [Given(@"Amy enters the following shipping address")]
        public void GivenAmyEntersTheFollowingShippingAddress(Table table)
        {
            var address = table.CreateSet<AddressDto>().FirstOrDefault();
            _customerInfo.ShippingAddress = address;
        }

        [When(@"Amy saves her contact details and shipping address")]
        public async Task WhenAmySavesHerContactDetailsAndShippingAddressAsync()
        {
            var url = $"api/checkout/{_context.ShopperId}/customerinfo";
            var apiResult = await _apiClient.PostAsync(url, _customerInfo);
            _context.ApiResult = apiResult;
        }

        [Then(@"Amy's information should be saved as expected")]
        public void ThenAmySInformationShouldBeSavedAsExpected()
        {
        }
    }
}
