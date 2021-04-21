using DuxCommerce.Core.Shared.PublicTypes;
using DuxCommerce.Core.Shipping.PublicTypes;
using DuxCommerce.Specifications.UseCases.Extensions;
using DuxCommerce.Specifications.UseCases.Hooks;
using DuxCommerce.Specifications.UseCases.Models;
using DuxCommerce.Specifications.Utilities;
using FluentAssertions;
using Newtonsoft.Json;
using System;
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
        private readonly StepContext _context;
        private readonly IApiClient _apiClient;

        private ShippingOriginDto _originCreated;

        private ShippingProfileDto _profileRequest;
        private ShippingProfileDto _profileCreated;

        public ShippingProfileSteps(StepContext context, IApiClient apiClient)
        {
            _context = context;
            _apiClient = apiClient;

            _profileRequest = new ShippingProfileDto();
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
        public async Task ThenShippingOriginShouldBeCreatedAsFollowAsync(Table table)
        {
            var ids = _profileCreated.OriginIds.Select(x => $"originIds={x}");
            var query = string.Join("&", ids);
            var url = $"api/shippingorigins?{query}";

            var apiResult = await _apiClient.GetAsync(url);
            var profileStr = await apiResult.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<ShippingOriginDto>>(profileStr);

            var expected = table.CreateSet<ShippingOrigin>();
            CompareOrigins(expected.ToList(), actual);
        }

        [Then(@"shipping zones should be created as follow:")]
        public void ThenShippingZonesShouldBeCreatedAsFollow(Table table)
        {
            var expected = table.CreateSet<ShippingZoneDto>();
            CompareZones(expected.ToList(), _profileCreated.Zones.ToList());
        }

        [Then(@"shippig countries should be created as follow:")]
        public void ThenShippigCountriesShouldBeCreatedAsFollow(Table table)
        {
            var expected = table.CreateSet<ZoneCountry>();
            CompareCountries(expected.ToList(), _profileCreated.Zones.SelectMany(x => x.Countries).ToList());
        }

        [Then(@"shippig states should be created as follow:")]
        public void ThenShippigStatesShouldBeCreatedAsFollow(Table table)
        {
            var expected = table.CreateSet<ZoneState>();
            var actual = _profileCreated.Zones.SelectMany(x => x.Countries).SelectMany(x => x.StateNames);
            expected.Select(x => x.Name).Should().BeEquivalentTo(actual);
        }

        [Given(@"Tom creates the following shipping origins:")]
        public async Task GivenTomCreatesTheFollowingShippingOriginsAsync(Table table)
        {
            var addressDto = table.CreateSet<AddressDto>().FirstOrDefault();
            var apiResult = await _apiClient.PostAsync("api/shippingorigins", addressDto);

            var resultStr = await apiResult.Content.ReadAsStringAsync();
            _originCreated = JsonConvert.DeserializeObject<ShippingOriginDto>(resultStr);
        }


        [When(@"Tom enters shipping profile name (.*)")]
        public void GivenTomEntersShippingProfileNameFragileProducts(string name)
        {
            _profileRequest.Name = name;
        }

        [When(@"Tom selects shipping origin (.*)")]
        public void GivenTomSelectsShippingOrigin(int originId)
        {
            _profileRequest.OriginIds = new List<string> { _originCreated.Id };
        }

        [When(@"Tom enters the zone name (.*)")]
        public void GivenTomEntersTheZoneNameANZ(string zoneName)
        {
            var zoneRquest = new ShippingZoneDto { Name = zoneName };
            _profileRequest.Zones = new List<ShippingZoneDto> { zoneRquest };
        }

        [When(@"Tom selects the following shipping countries:")]
        public void GivenTomSelectsTheFollowingShippingCountries(Table table)
        {
            var zoneCountries = table.CreateSet<ZoneCountry>();
            var zoneRequest = _profileRequest.Zones.FirstOrDefault();
            zoneRequest.Countries = zoneCountries
                .Select(c => new ShippingCountryDto { CountryCode = c.CountryCode })
                .ToList();
        }

        [When(@"Tom selects the following shipping states:")]
        public void GivenTomSelectsTheFollowingShippingStates(Table table)
        {
            var zoneStates = table.CreateSet<ZoneState>();
            var zoneRequest = _profileRequest.Zones.FirstOrDefault();
            foreach (var country in zoneRequest.Countries)
            {
                var states = zoneStates
                    .Where(s => s.CountryCode == country.CountryCode)
                    .Select(x => x.Name);

                country.StateNames = states;
            }
        }

        [When(@"Tom selects shipping method type (.*) and enters method name (.*)")]
        public void GivenTomSelectsRateTypeAndEntersRateName(string methodType, string methodName)
        {
            var zoneRequest = _profileRequest.Zones.FirstOrDefault();
            var method = new ShippingMethodDto { Id = Guid.NewGuid().ToString(),  Name = methodName, MethodType = methodType };
            zoneRequest.Methods = new List<ShippingMethodDto> { method };
        }

        [When(@"Tome enters the following rates:")]
        public void GivenTomeEntersTheFollowingRates(Table table)
        {
            var rates = table.CreateSet<ShippingRateDto>();
            var methodRequest = _profileRequest
                                .Zones.FirstOrDefault()
                                .Methods.FirstOrDefault();
            methodRequest.Rates = rates;
        }

        [When(@"Tom saves the shipping profile")]
        public async Task WhenTomSavesTheShippingProfileAsync()
        {
            var apiResult = await _apiClient.PostAsync("api/ShippingProfiles", _profileRequest);
            _context.ApiResult = apiResult;

            var profileStr = await apiResult.Content.ReadAsStringAsync();
            _profileCreated = JsonConvert.DeserializeObject<ShippingProfileDto>(profileStr);
        }

        [Then(@"custom shipping profile should be created as expected")]
        public void ThenCustomShippingProfileShouldBeCreatedAsExpected()
        {
            CompareZones(_profileRequest.Zones.ToList(), _profileCreated.Zones.ToList());

            var zoneRequest = _profileRequest.Zones.FirstOrDefault();

            var countries = _profileCreated.Zones.FirstOrDefault().Countries;
            CompareCountries(zoneRequest.Countries.ToList(), countries.ToList());

            var methods = _profileCreated.Zones.FirstOrDefault().Methods;
            CompareMethods(zoneRequest.Methods.ToList(), methods.ToList());

            _profileRequest.OriginIds.Should().BeEquivalentTo(_profileCreated.OriginIds);
        }

        private void CompareOrigins(List<ShippingOrigin> expected, List<ShippingOriginDto> actual)
        {
            expected.Count().Should().Be(actual.Count());
            expected.EqualTo(actual);
        }

        private void CompareZones(List<ShippingZoneDto> expected, List<ShippingZoneDto> actual)
        {
            expected.Count().Should().Be(actual.Count());
            for(var index = 0; index < expected.Count(); index ++)
            {
                expected[index].Name.Should().Be(actual[index].Name);
            }
        }

        private void CompareCountries(List<ZoneCountry> expected, List<ShippingCountryDto> actual)
        {
            expected.Count().Should().Be(actual.Count());
            for (var index = 0; index < expected.Count(); index++)
            {
                expected[index].CountryCode.Should().Be(actual[index].CountryCode);
            }
        }

        private void CompareCountries(List<ShippingCountryDto> expected, List<ShippingCountryDto> actual)
        {
            expected.Count().Should().Be(actual.Count());
            for (var index = 0; index < expected.Count(); index++)
            {
                expected[index].CountryCode.Should().Be(actual[index].CountryCode);
                expected[index].StateNames.Should().BeEquivalentTo(actual[index].StateNames);
            }
        }

        private void CompareMethods(List<ShippingMethodDto> expected, List<ShippingMethodDto> actual)
        {
            expected.Count().Should().Be(actual.Count());
            for (var index = 0; index < expected.Count(); index++)
            {
                expected[index].Name.Should().Be(actual[index].Name);
                expected[index].MethodType.Should().Be(actual[index].MethodType);

                CompareRates(expected[index].Rates.ToList(), actual[index].Rates.ToList());
            }
        }

        private void CompareRates(List<ShippingRateDto> expected, List<ShippingRateDto> actual)
        {
            expected.Count().Should().Be(actual.Count());
            for (var index = 0; index < expected.Count(); index++)
            {
                expected[index].Min.Should().Be(actual[index].Min);
                expected[index].Max.Should().Be(actual[index].Max);
                expected[index].Rate.Should().Be(actual[index].Rate);
            }
        }
    }
}
