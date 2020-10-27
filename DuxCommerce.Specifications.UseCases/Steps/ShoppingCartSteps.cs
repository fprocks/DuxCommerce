using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using DuxCommerce.ShoppingCarts;
using System.Collections.Generic;
using DuxCommerce.Specifications.UseCases.Hooks;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.FSharp.Collections;
using FluentAssertions;

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

            var lastApiResult = _context.ApiResults[_context.ApiResults.Count - 1];
            var resultStr = await lastApiResult.Content.ReadAsStringAsync();
            var shoppingCart = JsonConvert.DeserializeObject<CartInfo>(resultStr);
            _context.ShoppingCart = shoppingCart;
        }


        [Then(@"the shopping cart details should look like following:")]
        public void ThenTheShoppingCartDetailsShouldLookLikeFollowing(Table table)
        {
            var expectedItems = table.CreateSet<ExpectedCartItem>();
            var products = _context.CreatedProducts;
            foreach(var item in expectedItems)
            {
                var index = item.Product - 1;
                item.ProductId = products[index].Id;
            }

            CompareCartItems(expectedItems.ToList(), _context.ShoppingCart.LineItems);
        }

        [Then(@"the cart total is \$(.*)")]
        public void ThenTheCartTotalIs(int total)
        {
            _context.ShoppingCart.CartTotal.Should().Be(total);
        }

        private void CompareCartItems(List<ExpectedCartItem> expectedItems, FSharpList<CartItemInfo> lineItems)
        {
            expectedItems.Count().Should().Be(lineItems.Length);
            expectedItems.EqualTo(lineItems).Should().BeTrue();
        }

        private List<AddCartItemRequest> CreateRequests(List<AddToCartInput> inputs)
        {
            var requests = new List<AddCartItemRequest>();
            var products = _context.CreatedProducts;
            for(var index = 0; index < inputs.Count; index ++) {
                var productIndex = inputs[index].Product - 1;
                var productId = products[productIndex].Id;
                var quantity = inputs[index].Quantity;
                var request = new AddCartItemRequest(productId, quantity);

                requests.Add(request);
            }
            return requests;
        }
    }
}
