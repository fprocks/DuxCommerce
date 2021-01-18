using DuxCommerce.Core.Shared.PublicTypes;
using DuxCommerce.Core.Taxes.PublicTypes;
using DuxCommerce.Specifications.Models;
using DuxCommerce.Specifications.UseCases.Hooks;
using DuxCommerce.Specifications.UseCases.Models;
using DuxCommerce.Specifications.Utilities;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DuxCommerce.Specifications.Steps
{
    [Binding]
    public class TaxRateSteps
    {
        private readonly StepsContext _context;
        private readonly IApiClient _apiClient;

        private TaxRateDto _taxRateRequest;
        private TaxRateDto _taxRateCreated;

        public TaxRateSteps(StepsContext context, IApiClient apiClient)
        {
            _context = context;
            _apiClient = apiClient;
            _taxRateRequest = new TaxRateDto { Zone = new TaxZoneDto() };
        }

        [Given(@"Tom enters tax rate name (.*)")]
        public void GivenTomEntersTaxRateNameGST(string rateName)
        {
            _taxRateRequest.Name = rateName;
        }

        [Given(@"Tom enters tax zone name (.*)")]
        public void GivenTomEntersTaxZoneNameANZ(string zoneName)
        {
            _taxRateRequest.Zone.Name = zoneName;
        }

        [Given(@"Tom selects the following countries:")]
        public void GivenTomSelectsTheFollowingCountries(Table table)
        {
            var zoneCountries = table.CreateSet<ZoneCountry>();
            var taxCountries = zoneCountries.Select(
                x => new TaxCountryDto
                {
                    CountryCode = x.CountryCode,
                    States = new List<StateDto>(),
                    PostalCodes = new List<string>()
                }).ToList(); // Note: this is very important otherwise _taxRateRequest.Zone.Countries cannot be updated afterwards

            _taxRateRequest.Zone.Countries = taxCountries;
        }

        [Given(@"Tom selects the following states:")]
        public async Task GivenTomSelectsTheFollowingStatesAsync(Table table)
        {
            var zoneStates = table.CreateSet<ZoneState>();
            var allStates = await StateRepo.GetAllStatesAsync();
            var countries = _taxRateRequest.Zone.Countries;
            foreach (var zoneCountry in countries)
            {
                var stateNames = zoneStates
                    .Where(x => x.CountryCode == zoneCountry.CountryCode)
                    .Select(x => x.Name);

                var states = allStates.Where(x => stateNames.Contains(x.Name));
                zoneCountry.States = states;
            }
        }

        [Given(@"Tom enters the following postal codes:")]
        public void GivenTomEntersTheFollowingPostalCodes(Table table)
        {
            var zonePostalCodes = table.CreateSet<ZonePostalCodes>();
            foreach (var zoneCountry in _taxRateRequest.Zone.Countries)
            {
                var postalCodes = zonePostalCodes.FirstOrDefault(x => x.CountryCode == zoneCountry.CountryCode);
                var codes = postalCodes.PostalCodes.Split(',');
                zoneCountry.PostalCodes = codes;
            }
        }

        [Given(@"Tom enters tax rate amount (.*)")]
        public void GivenTomEntersTaxRateAmount(decimal amount)
        {
            _taxRateRequest.Amount = amount;
        }

        [When(@"Tom saves the tax rate")]
        public async Task WhenTomSavesTheTaxRateAsync()
        {
            var apiResult = await _apiClient.PostAsync("api/taxrates", _taxRateRequest);
            _context.ApiResult = apiResult;

            var taxRateStr = await apiResult.Content.ReadAsStringAsync();
            _taxRateCreated = JsonConvert.DeserializeObject<TaxRateDto>(taxRateStr);
        }

        [Then(@"Tax rate should be created as expected")]
        public void ThenTaxRateShouldBeCreatedAsExpected()
        {
            CompareRate(_taxRateRequest, _taxRateCreated);
        }

        private void CompareRate(TaxRateDto expected, TaxRateDto actual)
        {
            expected.Name.Should().BeEquivalentTo(actual.Name);
            expected.Amount.Should().Be(actual.Amount);

            CompareZone(expected.Zone, actual.Zone);
        }

        private void CompareZone(TaxZoneDto expected, TaxZoneDto actual)
        {
            expected.Name.Should().BeEquivalentTo(actual.Name);

            Comparer(expected.Countries, actual.Countries);
        }

        private void Comparer(IEnumerable<TaxCountryDto> expected, IEnumerable<TaxCountryDto> actual)
        {
            expected.Count().Should().Be(actual.Count());

            var expectedCountries = expected.ToList();
            var actualCountries = actual.ToList();
            for (var index = 0; index <= expected.Count() - 1; index++)
            {
                Compare(expectedCountries[index], actualCountries[index]);
            }
        }

        private void Compare(TaxCountryDto expected, TaxCountryDto actual)
        {
            expected.CountryCode.Should().BeEquivalentTo(actual.CountryCode);

            var expectedStates = expected.States.ToList();
            var actualStates = actual.States.ToList();
            for (var index = 0; index <= expectedStates.Count() - 1; index ++)
            {
                Compare(expectedStates[index], actualStates[index]);
            }

            var expectedPostalCodes = expected.PostalCodes.ToList();
            var actualPostalCodes = actual.PostalCodes.ToList();
            for (var index = 0; index <= expectedPostalCodes.Count() - 1; index++)
            {
                expectedPostalCodes[index].Should().BeEquivalentTo(actualPostalCodes[index]);
            }
        }

        private void Compare(StateDto expected, StateDto actual)
        {
            expected.Id.Should().BeEquivalentTo(actual.Id);
            expected.CountryCode.Should().BeEquivalentTo(actual.CountryCode);
            expected.Name.Should().BeEquivalentTo(actual.Name);
        }
    }
}
