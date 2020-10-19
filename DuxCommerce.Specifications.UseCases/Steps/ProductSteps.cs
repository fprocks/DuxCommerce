using DuxCommerce.Catalogue;
using DuxCommerce.Specifications.UseCases.Hooks;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace DuxCommerce.Specifications.UseCases.Steps
{
    [Binding]
    public class ProductSteps
    {
        private readonly Hooks.ScenarioContext _context;
        private readonly IApiClient _apiClient;

        public ProductSteps(Hooks.ScenarioContext context, IApiClient apiClient)
        {
            _context = context;
            _apiClient = apiClient;
        }

        [Given(@"Tom already created the following products:")]
        public async Task GivenTomAlreadyCreatedTheFollowingProductsAsync(Table table)
        {
            var requests = table.CreateSet<ProductInfo>();
            foreach (var request in requests)
            {
                await _apiClient.PostAsync("api/products", request);
            }
        }

        [Given(@"Tom enters the following product information:")]
        public void GivenTomEntersTheFollowingProductInformation(Table table)
        {
            var products = table.CreateSet<ProductInfo>();
            _context.ProductRequests.AddRange(products);
        }

        [When(@"Tom saves the products")]
        public async Task WhenTomSavesTheProductsAsync()
        {
            var productRequests = _context.ProductRequests;
            foreach(var request in productRequests)
            {
                var response = await _apiClient.PostAsync("api/products", request);
                _context.ApiResults.Add(response);
            }
        }

        [When(@"Tom updates the products")]
        public async Task WhenTomUpdatesTheProductsAsync()
        {
            var productRequests = _context.ProductRequests;
            foreach (var request in productRequests)
            {
                var response = await _apiClient.PutAsync("api/products", request);
                _context.ApiResults.Add(response);
            }
        }

        [Then(@"Tom should receive status codes (.*)")]
        public void ThenTomShouldReceiveSuccessResult(HttpStatusCode code)
        {
            var allOk = _context.ApiResults.All(x => x.StatusCode == code);
            allOk.Should().BeTrue();
        }

        [Then(@"the products should be created as follow:")]
        [Then(@"the products should be updated as follow:")]
        public async Task ThenTheProductsShouldBeCreatedAsFollowAsync(Table table)
        {
            var expected = table.CreateSet<ProductInfo>();

            var actual = new List<ProductInfo>();

            foreach(var response in _context.ApiResults)
            {
                var productStr = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<ProductInfo>(productStr);
                actual.Add(product);
            }

            actual.Count().Should().Be(expected.Count());
            actual.EqualTo(expected.ToList());
        }
    }
}
