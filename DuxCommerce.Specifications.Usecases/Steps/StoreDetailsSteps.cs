using DuxCommerce.Settings.PublicTypes;
using DuxCommerce.Specifications.UseCases.Hooks;
using FluentAssertions;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DuxCommerce.Specifications.UseCases.Steps
{
    [Binding]
    public class StoreDetailsSteps
    {
        private readonly Hooks.ScenarioContext _context;
        private readonly IApiClient _apiClient;

        private StoreDetailsDto _storeRequest;
        private StoreDetailsDto _actualStore;

        public StoreDetailsSteps(Hooks.ScenarioContext context, IApiClient apiClient)
        {
            _context = context;
            _apiClient = apiClient;
        }

        [Given(@"Tom already created the following store details:")]
        public void GivenTomAlreadyCreatedTheFollowingStoreDetails(Table table)
        {
            _storeRequest = table.CreateSet<StoreDetailsDto>().FirstOrDefault();
        }

        [Given(@"Tom already created the following store address:")]
        public async Task GivenTomAlreadyCreatedTheFollowingStoreAddressAsync(Table table)
        {
            _storeRequest.Address = table.CreateSet<AddressDto>().FirstOrDefault();

            var apiResult = await _apiClient.PostAsync("api/storedetails", _storeRequest);
            var storeDetails = await GetCreatedStoreDetails(apiResult);

            _context.CreatedStoreDetails = storeDetails;
        }

        [Given(@"Tom enters the following store details:")]
        public void GivenTomEntersTheFollowingStoreDetails(Table table)
        {
            _storeRequest = table.CreateSet<StoreDetailsDto>().FirstOrDefault();
        }
        
        [Given(@"Tome enters the following store address:")]
        public void GivenTomeEntersTheFollowingStoreAddress(Table table)
        {
            _storeRequest.Address = table.CreateSet<AddressDto>().FirstOrDefault();
        }
        
        [When(@"Tom saves the store details")]
        public async System.Threading.Tasks.Task WhenTomSavesTheStoreDetailsAsync()
        {
            var apiResult = await _apiClient.PostAsync("api/storedetails", _storeRequest);
            _context.ApiResult = apiResult;
        }

        [When(@"Tom updates the store details")]
        public async Task WhenTomUpdatesTheStoreDetailsAsync()
        {
            var storeId = _context.CreatedStoreDetails.Id;
            var apiResult = await _apiClient.PutAsync($"api/storedetails/{storeId}", _storeRequest);
            _context.ApiResult = apiResult;
        }

        [Then(@"the store details should be created as follow:")]
        public async Task ThenTheStoreDetailsShouldBeCreatedAsFollowAsync(Table table)
        {
            var apiResult = _context.ApiResult;
            _actualStore = await GetCreatedStoreDetails(apiResult);
            var expectedDetails = table.CreateSet<StoreDetailsDto>().FirstOrDefault();
            CompareStoreDetails(expectedDetails, _actualStore).Should().BeTrue();
        }

        [Then(@"the store address should be created as follow:")]
        public void ThenTheStoreAddressShouldBeCreatedAsFollow(Table table)
        {
            var expectedAddress = table.CreateSet<AddressDto>().FirstOrDefault();
            CompareStoreAddress(expectedAddress, _actualStore.Address).Should().BeTrue();
        }

        private async Task<StoreDetailsDto> GetCreatedStoreDetails(HttpResponseMessage apiResult)
        {
            var resultStr = await apiResult.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<StoreDetailsDto>(resultStr);
        }

        private bool CompareStoreDetails(StoreDetailsDto expected, StoreDetailsDto actual)
        {
            return expected.StoreName == actual.StoreName &&
                expected.ContactEmail == actual.ContactEmail &&
                expected.SenderEmail == actual.SenderEmail &&
                expected.BusinessName == actual.BusinessName &&
                expected.PhoneNumber == actual.PhoneNumber &&
                expected.TimeZoneId == actual.TimeZoneId &&
                expected.UnitSystem == actual.UnitSystem &&
                expected.WeightUnit == actual.WeightUnit &&
                expected.LengthUnit == actual.LengthUnit;
        }

        private bool CompareStoreAddress(AddressDto expected, AddressDto actual)
        {
            return expected.Address1 == actual.Address1 &&
                expected.Address2 == actual.Address2 &&
                expected.Address3 == actual.Address3 &&
                expected.City == actual.City &&
                expected.PostalCode == actual.PostalCode &&
                expected.State == actual.State &&
                expected.Country == actual.Country;
        }
    }
}
