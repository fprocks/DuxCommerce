using DuxCommerce.Settings.PublicTypes;
using DuxCommerce.Specifications.UseCases.Hooks;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DuxCommerce.Specifications.UseCases.Steps
{
    [Binding]
    public class StoreDetailsSteps
    {
        private readonly Hooks.ScenarioContext _context;
        private readonly IApiClient _apiClient;
        private StoreDetailsDto _storeDto;

        public StoreDetailsSteps(Hooks.ScenarioContext context, IApiClient apiClient)
        {
            _context = context;
            _apiClient = apiClient;
        }

        [Given(@"Tom enters the following store details:")]
        public void GivenTomEntersTheFollowingStoreDetails(Table table)
        {
            _storeDto = table.CreateSet<StoreDetailsDto>().FirstOrDefault();
        }
        
        [Given(@"Tome enters the following store address:")]
        public void GivenTomeEntersTheFollowingStoreAddress(Table table)
        {
            _storeDto.Address = table.CreateSet<AddressDto>().FirstOrDefault();
        }
        
        [When(@"Tom saves the store details")]
        public async System.Threading.Tasks.Task WhenTomSavesTheStoreDetailsAsync()
        {
            var apiResult = await _apiClient.PostAsync("api/storedetails", _storeDto);
            _context.ApiResults.Add(apiResult);
        }
    }
}
