using DuxCommerce.Core.Catalogue.PublicTypes;
using DuxCommerce.Specifications.UseCases.Extensions;
using DuxCommerce.Specifications.UseCases.Hooks;
using DuxCommerce.Specifications.Utilities;
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
    public class ProductSteps
    {
        private readonly StepContext _context;
        private readonly IApiClient _apiClient;

        private ProductDto _productRequest;

        public ProductSteps(StepContext context, IApiClient apiClient)
        {
            _context = context;
            _apiClient = apiClient;
        }

        [Given(@"Tom creates the following product:")]
        public async Task GivenTomCreatesTheFollowingProductAsync(Table table)
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
        [When(@"Tom enters the following product information:")]
        public void GivenTomEntersTheFollowingProductInformation(Table table)
        {
            _productRequest = table.CreateSet<ProductDto>().FirstOrDefault();
        }

        [When(@"Tom saves the product")]
        public async Task WhenTomSavesTheProductsAsync()
        {
            var apiResult = await _apiClient.PostAsync("api/products", _productRequest);
            _context.ApiResult = apiResult;
        }

        [When(@"Tom updates the product")]
        public async Task WhenTomUpdatesTheProductsAsync()
        {
            var id = _context.CreatedProducts[0].Id;
            var apiResult = await _apiClient.PutAsync($"api/products/{id}", _productRequest);
            _context.ApiResult = apiResult;
        }

        [Then(@"the product should be created as follow:")]
        public async Task ThenTheProductsShouldBeCreatedAsFollowAsync(Table table)
        {
            var createdProducts = await GetProduct();
            CompareProduct(table, createdProducts);
        }

        [Then(@"the product should be updated as follow:")]
        public async Task ThenTheProductsShouldBeUpdatedAsFollowAsync(Table table)
        {
            var updatedProducts = await GetProduct();
            CompareProduct(table, updatedProducts);
        }

        private async Task<ProductDto> GetProduct()
        {
            var apiResult = _context.ApiResult;
            return await GetProduct(apiResult);
        }

        private void CompareProduct(Table table, ProductDto actual)
        {
            var expected = table.CreateSet<ProductDto>().FirstOrDefault();

            expected.EqualTo(actual).Should().BeTrue();
        }

        private async Task<ProductDto> GetProduct(HttpResponseMessage apiResult)
        {
            var resultStr = await apiResult.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProductDto>(resultStr);
        }
    }
}
