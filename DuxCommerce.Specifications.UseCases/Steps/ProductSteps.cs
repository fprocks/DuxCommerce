using DuxCommerce.Catalogue;
using DuxCommerce.Specifications.UseCases.Hooks;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
                var apiResult = await _apiClient.PostAsync("api/products", request);
                var product = await GetProduct(apiResult);
                _context.CreatedProducts.Add(product);
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
            var requests = _context.ProductRequests;
            for (var index = 0; index < requests.Count; index ++)
            {
                var id = _context.CreatedProducts[index].Id;
                var response = await _apiClient.PutAsync($"api/products/{id}", requests[index]);
                _context.ApiResults.Add(response);
            }
        }

        [Then(@"Tom should receive status codes (.*)")]
        public void ThenTomShouldReceiveSuccessResult(HttpStatusCode code)
        {
            var codesMatch = _context.ApiResults.All(x => x.StatusCode == code);
            codesMatch.Should().BeTrue();
        }

        [Then(@"the products should be created as follow:")]
        public async Task ThenTheProductsShouldBeCreatedAsFollowAsync(Table table)
        {
            var createdProducts = await GetCreateProducts();
            CompareProducts(table, createdProducts);
        }

        [Then(@"the products should be updated as follow:")]
        public async Task ThenTheProductsShouldBeUpdatedAsFollowAsync(Table table)
        {
            var updatedProducts = await GetUpdatedProductsAsync();
            CompareProducts(table, updatedProducts);
        }

        private async Task<List<ProductInfo>> GetCreateProducts()
        {
            var ids = await GetCreatedProductIds();
            return await GetProducts(ids);
        }
        private async Task<List<ProductInfo>> GetUpdatedProductsAsync()
        {
            var ids = _context.CreatedProducts.Select(x => x.Id);
            return await GetProducts(ids);
        }

        private void CompareProducts(Table table, List<ProductInfo> actual)
        {
            var expected = table.CreateSet<ProductInfo>();

            actual.Count().Should().Be(expected.Count());
            actual.EqualTo(expected.ToList());
        }

        private async Task<List<long>> GetCreatedProductIds()
        {
            var productIds = new List<long>();
            foreach (var apiResult in _context.ApiResults)
            {
                var product = await GetProduct(apiResult);
                productIds.Add(product.Id);
            }

            return productIds;
        }

        private async Task<List<ProductInfo>> GetProducts(IEnumerable<long> productIds)
        {
            var actual = new List<ProductInfo>();
            foreach (var id in productIds)
            {
                var response = await _apiClient.GetAsync($"api/products/{id}");
                var product = await GetProduct(response);
                actual.Add(product);
            }

            return actual;
        }

        private async Task<ProductInfo> GetProduct(HttpResponseMessage apiResult)
        {
            var resultStr = await apiResult.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProductInfo>(resultStr);
        }
    }
}
