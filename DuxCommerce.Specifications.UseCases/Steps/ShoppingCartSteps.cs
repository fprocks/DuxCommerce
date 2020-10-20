using System;
using TechTalk.SpecFlow;

namespace DuxCommerce.Specifications.UseCases.Steps
{
    [Binding]
    public class ShoppingCartSteps
    {
        [When(@"Amy adds the following products to her cart:")]
        public void WhenAmyAddsTheFollowingProductsToHerCart(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the shopping cart details should look like following:")]
        public void ThenTheShoppingCartDetailsShouldLookLikeFollowing(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the cart total is \$(.*)")]
        public void ThenTheCartTotalIs(int p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
