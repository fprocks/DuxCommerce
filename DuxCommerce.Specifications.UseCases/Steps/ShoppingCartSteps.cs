using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using DuxCommerce.ShoppingCarts;
using System.Collections.Generic;
using DuxCommerce.Specifications.UseCases.Hooks;
using System.Linq;
using Newtonsoft.Json;
using FluentAssertions;
using System.Net.Http;
using System.Threading.Tasks;

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

        [When(@"Amy adds the following products to her shopping cart:")]
        [Given(@"Amy adds the following products to her shopping cart:")]
        public async Task WhenAmyAddsTheFollowingProductsToHerShoppingCartAsync(Table table)
        {
            var inputs = table.CreateSet<AddToCartInput>();
            var requests = CreateAddToCartRequests(inputs.ToList());

            HttpResponseMessage lastApiResult = null;
            var url = $"api/shoppingcart/{_context.ShopperId}/items";
            foreach (var request in requests)
            {
                lastApiResult = await _apiClient.PostAsync(url, request);
            }

            var resultStr = await lastApiResult.Content.ReadAsStringAsync();
            var shoppingCart = JsonConvert.DeserializeObject<CartInfo>(resultStr);
            _context.ShoppingCart = shoppingCart;
        }

        [When(@"Amy updates her shopping cart as follow:")]
        public async Task WhenAmyUpdatesHerShoppingCartAsFollowAsync(Table table)
        {
            var inputs = table.CreateSet<UpdateCartItemInput>();
            var request = CreateUpdateCartRequest(inputs.ToList());

            var url = $"api/shoppingcart/{_context.ShopperId}";
            var apiResult = await _apiClient.PutAsync(url, request);

            var resultStr = await apiResult.Content.ReadAsStringAsync();
            var shoppingCart = JsonConvert.DeserializeObject<CartInfo>(resultStr);
            _context.ShoppingCart = shoppingCart;
        }

        [When(@"Amy deletes the following cart items:")]
        public async void WhenAmyDeletesTheFollowingCartItemsAsync(Table table)
        {
            var inputs = table.CreateSet<DeleteCartItemInput>();
            var requests = CreateDeleteCartItemRequests(inputs.ToList());

            HttpResponseMessage lastApiResult = null;
            var url = $"api/shoppingcart/{_context.ShopperId}/items";
            foreach (var request in requests)
            {
                lastApiResult = await _apiClient.DeleteAsync(url, request);
            }

            var resultStr = await lastApiResult.Content.ReadAsStringAsync();
            var shoppingCart = JsonConvert.DeserializeObject<CartInfo>(resultStr);
            _context.ShoppingCart = shoppingCart;
        }

        [Then(@"her cart details should look as follow:")]
        public void ThenHerCartDetailsShouldLookAsFollow(Table table)
        {
            var expectedItems = table.CreateSet<ExpectedCartItem>();
            var products = _context.CreatedProducts;
            foreach (var item in expectedItems)
            {
                var index = item.Product - 1;
                item.ProductId = products[index].Id;
            }

            CompareCartItems(expectedItems.ToList(), _context.ShoppingCart.LineItems.ToList());
        }

        [Then(@"the cart total is \$(.*)")]
        public void ThenTheCartTotalIs(int total)
        {
            _context.ShoppingCart.CartTotal.Should().Be(total);
        }

        private void CompareCartItems(List<ExpectedCartItem> expectedItems, List<CartItemInfo> lineItems)
        {
            expectedItems.Count().Should().Be(lineItems.Count());
            expectedItems.EqualTo(lineItems).Should().BeTrue();
        }

        private List<AddCartItemRequest> CreateAddToCartRequests(List<AddToCartInput> inputs)
        {
            var requests = new List<AddCartItemRequest>();
            var products = _context.CreatedProducts;
            for(var index = 0; index < inputs.Count; index ++) 
            {
                var productIndex = inputs[index].Product - 1;
                var productId = products[productIndex].Id;
                var quantity = inputs[index].Quantity;

                requests.Add(new AddCartItemRequest(productId, quantity));
            }
            return requests;
        }

        private UpdateCartRequest CreateUpdateCartRequest(List<UpdateCartItemInput> inputs)
        {
            var requests = new List<UpdateCartItemRequest>();
            var products = _context.CreatedProducts;
            for(var index = 0; index < inputs.Count; index ++)
            {
                var productIndex = inputs[index].Product - 1;
                var productId = products[productIndex].Id;
                var quantity = inputs[index].Quantity;

                requests.Add(new UpdateCartItemRequest(productId, quantity));
            }

            return new UpdateCartRequest(requests);
        }

        private List<DeleteCartItemRequest> CreateDeleteCartItemRequests(List<DeleteCartItemInput> inputs)
        {
            var requests = new List<DeleteCartItemRequest>();
            var products = _context.CreatedProducts;
            for (var index = 0; index < inputs.Count; index++)
            {
                var productIndex = inputs[index].Product - 1;
                var productId = products[productIndex].Id;

                requests.Add(new DeleteCartItemRequest(productId));
            }
            return requests;
        }
    }
}
