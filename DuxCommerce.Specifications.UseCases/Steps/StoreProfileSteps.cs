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
    public class StoreProfileSteps
    {
        private readonly StepsContext _context;
        private readonly IApiClient _apiClient;

        private StoreProfileDto _profilePreUpdate;

        private StoreProfileDto _profileRequest;
        private StoreProfileDto _profilePostUpdate;

        public StoreProfileSteps(StepsContext context, IApiClient apiClient)
        {
            _context = context;
            _apiClient = apiClient;
        }

        [Given(@"Tom already created the following store profile:")]
        public void GivenTomAlreadyCreatedTheFollowingStoreProfile(Table table)
        {
            _profileRequest = table.CreateSet<StoreProfileDto>().FirstOrDefault();
        }

        [Given(@"Tom already created the following store address:")]
        public async Task GivenTomAlreadyCreatedTheFollowingStoreAddressAsync(Table table)
        {
            _profileRequest.Address = table.CreateSet<AddressDto>().FirstOrDefault();

            var apiResult = await _apiClient.PostAsync("api/storeprofile", _profileRequest);
            var storeProfile = await GetCreatedProfile(apiResult);

            _profilePreUpdate = storeProfile;
        }

        [Given(@"Tom enters the following store profile:")]
        public void GivenTomEntersTheFollowingStoreProfile(Table table)
        {
            _profileRequest = table.CreateSet<StoreProfileDto>().FirstOrDefault();
        }
        
        [Given(@"Tome enters the following store address:")]
        public void GivenTomeEntersTheFollowingStoreAddress(Table table)
        {
            var address = table.CreateSet<AddressDto>().FirstOrDefault();
            address.FirstName = "F";
            address.LastName = "L";
            _profileRequest.Address = address;
        }
        
        [When(@"Tom saves the store profile")]
        public async Task WhenTomSavesTheStoreProfileAsync()
        {
            var apiResult = await _apiClient.PostAsync("api/storeprofile", _profileRequest);
            _context.ApiResult = apiResult;
        }

        [When(@"Tom updates the store profile")]
        public async Task WhenTomUpdatesTheStoreProfileAsync()
        {
            var url = $"api/storeprofile/{_profilePreUpdate.Id}";
            var apiResult = await _apiClient.PutAsync(url, _profileRequest);
            _context.ApiResult = apiResult;
        }

        [Then(@"the store profile should be created as follow:")]
        [Then(@"the store profile should be updated as follow:")]
        public async Task ThenTheStoreDetailsShouldBeCreatedAsFollowAsync(Table table)
        {
            _profilePostUpdate = await GetCreatedProfile(_context.ApiResult);
            var expectedProfile = table.CreateSet<StoreProfileDto>().FirstOrDefault();

            CompareStoreProfile(expectedProfile, _profilePostUpdate).Should().BeTrue();
        }

        [Then(@"the store address should be created as follow:")]
        [Then(@"the store address should be updated as follow:")]
        public void ThenTheStoreAddressShouldBeCreatedAsFollow(Table table)
        {
            var expectedAddress = table.CreateSet<AddressDto>().FirstOrDefault();
            CompareStoreAddress(expectedAddress, _profilePostUpdate.Address).Should().BeTrue();
        }

        private async Task<StoreProfileDto> GetCreatedProfile(HttpResponseMessage apiResult) 
        {
            var resultStr = await apiResult.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<StoreProfileDto>(resultStr);
        }

        private bool CompareStoreProfile(StoreProfileDto expected, StoreProfileDto actual)
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
            return expected.AddressLine1 == actual.AddressLine1 &&
                expected.AddressLine2 == actual.AddressLine2 &&
                expected.City == actual.City &&
                expected.PostalCode == actual.PostalCode &&
                expected.StateName == actual.StateName &&
                expected.CountryCode == actual.CountryCode;
        }
    }
}