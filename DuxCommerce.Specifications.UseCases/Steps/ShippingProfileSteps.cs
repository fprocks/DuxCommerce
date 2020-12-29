using DuxCommerce.Settings.PublicTypes;
using DuxCommerce.Specifications.UseCases.Hooks;
using DuxCommerce.Specifications.UseCases.Model;
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
        private ShippingProfileDto _shippingProfile;

        public ShippingProfileSteps(StepsContext context, IApiClient apiClient)
        {
            _context = context;
            _apiClient = apiClient;
        }

        [Then(@"default shipping profile should be created as follow:")]
        public async Task ThenDefaultShippingProfileShouldBeCreatedAsFollowAsync(Table table)
        {
            var url = $"api/shippingprofile/default";
            var apiResult = await _apiClient.GetAsync(url);
            var profileStr = await apiResult.Content.ReadAsStringAsync();

            _shippingProfile = JsonConvert.DeserializeObject<ShippingProfileDto>(profileStr);

            var expected = table.CreateSet<ExpectedShippingProfile>().FirstOrDefault();
            expected.Name.Should().Be(_shippingProfile.Name);
            expected.IsDefault.Should().Be(_shippingProfile.IsDefault);
        }

        [Then(@"shipping origin should be created as follow:")]
        public void ThenShippingOriginShouldBeCreatedAsFollow(Table table)
        {
            var expected = table.CreateSet<ExpectedOrigin>();
            CompareOrigins(expected.ToList(), _shippingProfile.Origins.ToList());
        }

        [Then(@"shipping zone should be created as follow:")]
        public void ThenShippingZoneShouldBeCreatedAsFollow(Table table)
        {
            var expected = table.CreateSet<ExpectedZone>();
            CompareZones(expected.ToList(), _shippingProfile.Zones.ToList());
        }

        [Then(@"shippig countries should be created as follow:")]
        public void ThenShippigCountriesShouldBeCreatedAsFollow(Table table)
        {
        }

        [Then(@"shippig states should be created as follow:")]
        public void ThenShippigStatesShouldBeCreatedAsFollow(Table table)
        {
        }

        private void CompareOrigins(List<ExpectedOrigin> expected, List<ShippingOriginDto> actual)
        {
            expected.Count().Should().Be(actual.Count());
            expected.Equals(actual);
        }

        private void CompareZones(List<ExpectedZone> expected, List<ShippingZoneDto> actual)
        {
            expected.Count().Should().Be(actual.Count());
            for(var index = 0; index < expected.Count(); index ++)
            {
                expected[index].Name.Should().Be(actual[index].Name);
            }
        }
    }
}
