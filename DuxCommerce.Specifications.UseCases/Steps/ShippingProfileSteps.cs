using DuxCommerce.Settings.Dto;
using DuxCommerce.Settings.PublicTypes;
using DuxCommerce.Specifications.UseCases.Hooks;
using DuxCommerce.Specifications.UseCases.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DuxCommerce.Specifications.UseCases.Steps
{
    [Binding]
    public class ShippingProfileSteps
    {
        private readonly StepsContext _context;
        private readonly IApiClient _apiClient;

        private ShippingProfileDto _profileCreated;

        private ShippingOriginDto _originCreated;
        private ShippingProfileRequest _profileRequest;

        private ShippingZoneRequest _zoneRquest;
        private List<ShippingCountryRequest> _countryRequests;
        private ShippingRateRequest _rateRequest;

        public ShippingProfileSteps(StepsContext context, IApiClient apiClient)
        {
            _context = context;
            _apiClient = apiClient;

            _profileRequest = new ShippingProfileRequest();
        }

        [Then(@"default shipping profile should be created as follow:")]
        public async Task ThenDefaultShippingProfileShouldBeCreatedAsFollowAsync(Table table)
        {
            var url = $"api/shippingprofiles/default";
            var apiResult = await _apiClient.GetAsync(url);
            var profileStr = await apiResult.Content.ReadAsStringAsync();

            _profileCreated = JsonConvert.DeserializeObject<ShippingProfileDto>(profileStr);

            var expected = table.CreateSet<ShippingProfile>().FirstOrDefault();
            expected.Name.Should().Be(_profileCreated.Name);
            expected.IsDefault.Should().Be(_profileCreated.IsDefault);
        }

        [Then(@"shipping origin should be created as follow:")]
        public void ThenShippingOriginShouldBeCreatedAsFollow(Table table)
        {
            var expected = table.CreateSet<ShippingOrigin>();
            CompareOrigins(expected.ToList(), _profileCreated.Origins.ToList());
        }

        [Then(@"shipping zones should be created as follow:")]
        public void ThenShippingZonesShouldBeCreatedAsFollow(Table table)
        {
            var expected = table.CreateSet<ShippingZone>();
            CompareZones(expected.ToList(), _profileCreated.Zones.ToList());
        }

        [Then(@"shippig countries should be created as follow:")]
        public void ThenShippigCountriesShouldBeCreatedAsFollow(Table table)
        {
            var expected = table.CreateSet<ShippingCountry>();
            CompareCountries(expected.ToList(), _profileCreated.Zones.SelectMany(x => x.Countries).ToList());
        }

        [Then(@"shippig states should be created as follow:")]
        public void ThenShippigStatesShouldBeCreatedAsFollow(Table table)
        {
            var expected = table.CreateSet<ShippingState>();
            var actual = _profileCreated.Zones.SelectMany(x => x.Countries).SelectMany(x => x.States);
            CompareStates(expected.ToList(), actual.ToList());
        }

        [Given(@"Tom already created the following shipping origins:")]
        public async Task GivenTomAlreadyCreatedTheFollowingShippingOriginsAsync(Table table)
        {
            var addressDto = table.CreateSet<AddressDto>().FirstOrDefault();
            var apiResult = await _apiClient.PostAsync("api/shippingorigins", addressDto);

            var resultStr = await apiResult.Content.ReadAsStringAsync();
            _originCreated = JsonConvert.DeserializeObject<ShippingOriginDto>(resultStr);
        }

        [Given(@"Tom enters shipping profile name (.*)")]
        public void GivenTomEntersShippingProfileNameFragileProducts(string name)
        {
            _profileRequest.Name = name;
        }

        [Given(@"Tom selects shipping origin (.*)")]
        public void GivenTomSelectsShippingOrigin(int originId)
        {
            _profileRequest.Origins = new List<long> { _originCreated.Id };
        }

        [Given(@"Tom enters the zone name (.*)")]
        public void GivenTomEntersTheZoneNameANZ(string zoneName)
        {
            _zoneRquest = new ShippingZoneRequest { Name = zoneName };
        }

        [Given(@"Tom selects the following shipping countries:")]
        public void GivenTomSelectsTheFollowingShippingCountries(Table table)
        {
            var countryCodes = table.CreateSet<ShippingCountry>();
            _countryRequests = countryCodes
                .Select(c => new ShippingCountryRequest { CountryCode = c.CountryCode })
                .ToList();
        }

        [Given(@"Tom selects the following shipping states:")]
        public void GivenTomSelectsTheFollowingShippingStates(Table table)
        {
            var shippingStates = table.CreateSet<ShippingState>();
            foreach(var country in _countryRequests)
            {
                var states = shippingStates
                    .Where(s => s.CountryCode == country.CountryCode)
                    .Select(x => x.StateId);

                country.States = states;
            }

            _zoneRquest.Countries = _countryRequests;
        }

        [Given(@"Tom selects rate type (.*) and enters rate name (.*)")]
        public void GivenTomSelectsRateTypeAndEntersRateName(string rateType, string rateName)
        {
            _rateRequest = new ShippingRateRequest { Ratetype = rateType, Name = rateName };
        }

        [Given(@"Tome enters the following rates:")]
        public void GivenTomeEntersTheFollowingRates(Table table)
        {
            var rateItems = table.CreateSet<ShippingRateItemRequest>();
            _rateRequest.Items = rateItems;

            _zoneRquest.Rates = new List<ShippingRateRequest> { _rateRequest };
            _profileRequest.Zones = new List<ShippingZoneRequest> { _zoneRquest };
        }

        [When(@"Tom saves the shipping profile")]
        public async Task WhenTomSavesTheShippingProfileAsync()
        {
            var apiResult = await _apiClient.PostAsync("api/ShippingProfiles", _profileRequest);
            _context.ApiResult = apiResult;
        }

        [Then(@"shipping profile should be saved as expected")]
        public void ThenShippingProfileShouldBeSavedAsExpected()
        {
        }

        private void CompareOrigins(List<ShippingOrigin> expected, List<ShippingOriginDto> actual)
        {
            expected.Count().Should().Be(actual.Count());
            expected.Equals(actual);
        }

        private void CompareZones(List<ShippingZone> expected, List<ShippingZoneDto> actual)
        {
            expected.Count().Should().Be(actual.Count());
            for(var index = 0; index < expected.Count(); index ++)
            {
                expected[index].Name.Should().Be(actual[index].Name);
            }
        }

        private void CompareCountries(List<ShippingCountry> expected, List<ShippingCountryDto> actual)
        {
            expected.Count().Should().Be(actual.Count());
            for (var index = 0; index < expected.Count(); index++)
            {
                expected[index].CountryCode.Should().Be(actual[index].CountryCode);
            }
        }

        private void CompareStates(List<ShippingState> expected, List<StateDto> actual)
        {
            expected.Count().Should().Be(actual.Count());
            for (var index = 0; index < expected.Count(); index++)
            {
                expected[index].CountryCode.Should().Be(actual[index].CountryCode);
                expected[index].Name.Should().Be(actual[index].Name);
            }
        }
    }
}
