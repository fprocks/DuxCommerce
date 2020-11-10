using DuxCommerce.Catalogue.PublicTypes;
using DuxCommerce.Specifications.UseCases.Extensions;
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
        [Given(@"the following products are already created:")]
        public async Task GivenTomAlreadyCreatedTheFollowingProductsAsync(Table table)
        {
            var requests = table.CreateSet<ProductDto>();
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
            var products = table.CreateSet<ProductDto>();
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
        [Then(@"she should receive status codes (.*)")]
        public void ThenTomShouldReceiveSuccessResult(HttpStatusCode code)
        {
            var codesMatch = _context.ApiResults.All(x => x.StatusCode == code);
            codesMatch.Should().BeTrue();
        }

        [Then(@"the products should be created as follow:")]
        public async Task ThenTheProductsShouldBeCreatedAsFollowAsync(Table table)
        {
            var createdProducts = await GetProducts();
            CompareProducts(table, createdProducts);
        }

        [Then(@"the products should be updated as follow:")]
        public async Task ThenTheProductsShouldBeUpdatedAsFollowAsync(Table table)
        {
            var updatedProducts = await GetProducts();
            CompareProducts(table, updatedProducts);
        }

        private async Task<List<ProductDto>> GetProducts()
        {
            var products = new List<ProductDto>();
            foreach(var apiResult in _context.ApiResults)
            {
                var product = await GetProduct(apiResult);
                products.Add(product);
            }
            return products;
        }

        private void CompareProducts(Table table, List<ProductDto> actualProducts)
        {
            var expectedProducts = table.CreateSet<ProductDto>();

            actualProducts.Count().Should().Be(expectedProducts.Count());
            actualProducts.EqualTo(expectedProducts.ToList());
        }

        private async Task<ProductDto> GetProduct(HttpResponseMessage apiResult)
        {
            var resultStr = await apiResult.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProductDto>(resultStr);
        }
    }
}
