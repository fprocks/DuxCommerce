using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using DuxCommerce.ShoppingCarts;
using System.Collections.Generic;
using DuxCommerce.Specifications.UseCases.Hooks;
using System.Linq;

namespace DuxCommerce.Specifications.UseCases.Steps
{
    [Binding]
    public class ShoppingCartSteps
    {
        private readonly Hooks.ScenarioContext _context;
        private readonly IApiClient _apiClient;

        public ShoppingCartSteps(Hooks.ScenarioContext context, IApiClient apiClient)
        {
            _context = context;
            _apiClient = apiClient;
        }

        [When(@"Amy adds the following products to her cart:")]
        public async System.Threading.Tasks.Task WhenAmyAddsTheFollowingProductsToHerCartAsync(Table table)
        {
            var inputs = table.CreateSet<AddToCartInput>();
            var requests = CreateRequests(inputs.ToList());

            foreach(var request in requests)
            {
                var response = await _apiClient.PostAsync("api/shoppingcart/items", request);
                _context.ApiResults.Add(response);
            }
        }


        [Then(@"the shopping cart details should look like following:")]
        public void ThenTheShoppingCartDetailsShouldLookLikeFollowing(Table table)
        {
        }

        [Then(@"the cart total is \$(.*)")]
        public void ThenTheCartTotalIs(int total)
        {
        }

        private List<AddCartItemRequest> CreateRequests(List<AddToCartInput> inputs)
        {
            var requests = new List<AddCartItemRequest>();
            var products = _context.CreatedProducts;
            for(var index = 0; index < inputs.Count; index ++) {
                var productId = products[inputs[index].Product].Id;
                var quantity = inputs[index].Quantity;
                var request = new AddCartItemRequest(productId, quantity);

                requests.Add(request);
            }
            return requests;
        }
    }
}
