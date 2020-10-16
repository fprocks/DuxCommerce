using DuxCommerce.Catalogue;
using DuxCommerce.Specifications.UseCases.Hooks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DuxCommerce.Specifications.UseCases.Steps
{
    [Binding]
    public class ProductSteps
    {
        private readonly MyScenarioContext _context;

        public ProductSteps(MyScenarioContext context)
        {
            _context = context;
        }

        [Given(@"Tom enters the following product information:")]
        public void GivenTomEntersTheFollowingProductInformation(Table table)
        {
            var products = table.CreateSet<ProductInfo>();
            _context.ProductInfoList.AddRange(products);
        }

        [When(@"Tom saves the products")]
        public void WhenTomSavesTheProducts()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"Tom should receive success result")]
        public void ThenTomShouldReceiveSuccessResult()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the products should be created as follow:")]
        public void ThenTheProductsShouldBeCreatedAsFollow(Table table)
        {
            ScenarioContext.Current.Pending();
        }

    }
}
