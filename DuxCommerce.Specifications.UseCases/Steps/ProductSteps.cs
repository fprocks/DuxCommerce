using DuxCommerce.Catalogue;
using DuxCommerce.Specifications.UseCases.Hooks;
using System.Collections.Generic;
using System.Linq;
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

        [Given(@"Tom enters the following product information:")]
        public void GivenTomEntersTheFollowingProductInformation(Table table)
        {
            var products = table.CreateSet<ProductInfo>();
            _context.ProductRequests.AddRange(products);
        }

        [When(@"Tom saves the products")]
        public async Task WhenTomSavesTheProductsAsync()
        {
            var result = new List<object>();
            var productRequests = _context.ProductRequests;
            foreach(var request in productRequests)
            {
                var response = await _apiClient.PostAsync("api/products", request);
                result.Add(response);
            }
        }

        [Then(@"Tom should receive success result")]
        public void ThenTomShouldReceiveSuccessResult()
        {
            TechTalk.SpecFlow.ScenarioContext.Current.Pending();
        }

        [Then(@"the products should be created as follow:")]
        public void ThenTheProductsShouldBeCreatedAsFollow(Table table)
        {
            TechTalk.SpecFlow.ScenarioContext.Current.Pending();
        }

    }
}
