using DuxCommerce.Catalogue;
using DuxCommerce.Specifications.UseCases.Helpers;
using System;
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
            this._context = context;
        }

        [Given(@"Tom enters the following product information:")]
        public void GivenTomEntersTheFollowingProductInformation(Table table)
        {
            var products = table.CreateSet<ProductInfo>();
            _context.ProductInfoList.AddRange(products);
        }
    }
}
