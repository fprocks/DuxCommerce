using DuxCommerce.Settings.PublicTypes;
using DuxCommerce.Specifications.UseCases.Hooks;
using FluentAssertions;
using Newtonsoft.Json;
using System.Linq;
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
        private StoreDetailsDto _storeDetailsInput;
        private StoreDetailsDto _createdDetails;

        public StoreDetailsSteps(Hooks.ScenarioContext context, IApiClient apiClient)
        {
            _context = context;
            _apiClient = apiClient;
        }

        [Given(@"Tom enters the following store details:")]
        public void GivenTomEntersTheFollowingStoreDetails(Table table)
        {
            _storeDetailsInput = table.CreateSet<StoreDetailsDto>().FirstOrDefault();
        }
        
        [Given(@"Tome enters the following store address:")]
        public void GivenTomeEntersTheFollowingStoreAddress(Table table)
        {
            _storeDetailsInput.Address = table.CreateSet<AddressDto>().FirstOrDefault();
        }
        
        [When(@"Tom saves the store details")]
        public async System.Threading.Tasks.Task WhenTomSavesTheStoreDetailsAsync()
        {
            var apiResult = await _apiClient.PostAsync("api/storedetails", _storeDetailsInput);
            _context.ApiResults.Add(apiResult);
        }

        [Then(@"the store details should be created as follow:")]
        public async Task ThenTheStoreDetailsShouldBeCreatedAsFollowAsync(Table table)
        {
            _createdDetails = await GetCreatedStoreDetails();
            var expectedDetails = table.CreateSet<StoreDetailsDto>().FirstOrDefault();
            CompareStoreDetails(expectedDetails, _createdDetails).Should().BeTrue();
        }

        [Then(@"the store address should be created as follow:")]
        public void ThenTheStoreAddressShouldBeCreatedAsFollow(Table table)
        {
            var expectedAddress = table.CreateSet<AddressDto>().FirstOrDefault();
            CompareStoreAddress(expectedAddress, _createdDetails.Address).Should().BeTrue();
        }

        private async Task<StoreDetailsDto> GetCreatedStoreDetails()
        {
            var apiResult = _context.ApiResults[0];
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
