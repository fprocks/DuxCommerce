using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using System.Collections.Generic;
using DuxCommerce.Specifications.UseCases.Hooks;
using System.Linq;
using Newtonsoft.Json;
using FluentAssertions;
using System.Net.Http;
using System.Threading.Tasks;
using DuxCommerce.Specifications.UseCases.Extensions;
using DuxCommerce.Specifications.UseCases.Forms;
using DuxCommerce.ShoppingCarts.PublicTypes;
using System.Net;

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

        [Given(@"Amy adds the following products to her shopping cart:")]
        public async Task GivenAmyAddsTheFollowingProductsToHerShoppingCartAsync(Table table)
        {
            var lastApiResult = await AddCartItems(table);
            _context.ShoppingCart = await GetShoppingCart(lastApiResult);
        }

        [When(@"Amy adds the following products to her shopping cart:")]
        public async Task WhenAmyAddsTheFollowingProductsToHerShoppingCartAsync(Table table)
        {
            var lastApiResult = await AddCartItems(table);
            _context.ShoppingCart = await GetShoppingCart(lastApiResult);
            _context.ApiResults.Add(lastApiResult);
        }

        [When(@"Amy updates her shopping cart as follow:")]
        public async Task WhenAmyUpdatesHerShoppingCartAsFollowAsync(Table table)
        {
            HttpResponseMessage apiResult = await UpdateShoppingCart(table);
            _context.ShoppingCart = await GetShoppingCart(apiResult);
            _context.ApiResults.Add(apiResult);
        }

        [When(@"Amy deletes the following cart items:")]
        public async Task WhenAmyDeletesTheFollowingCartItemsAsync(Table table)
        {
            var lastApiResult = await DeleteCartItems(table);
            var shoppingCart = await GetShoppingCart(lastApiResult);
            _context.ShoppingCart = shoppingCart;
        }

        [Then(@"her cart details should look as follow:")]
        public void ThenHerCartDetailsShouldLookAsFollow(Table table)
        {
            var statusOK = _context.ApiResults.All(x => x.StatusCode == HttpStatusCode.OK);
            statusOK.Should().BeTrue();

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

        private async Task<HttpResponseMessage> AddCartItems(Table table)
        {
            var inputs = table.CreateSet<AddToCartForm>();
            var requests = CreateAddCartItemRequests(inputs.ToList());

            HttpResponseMessage lastApiResult = null;
            var url = $"api/shoppingcart/{_context.ShopperId}/items";
            foreach (var request in requests)
            {
                lastApiResult = await _apiClient.PostAsync(url, request);
            }

            return lastApiResult;
        }

        private async Task<HttpResponseMessage> UpdateShoppingCart(Table table)
        {
            var inputs = table.CreateSet<UpdateCartItemForm>();
            var request = CreateUpdateCartRequest(inputs.ToList());

            var url = $"api/shoppingcart/{_context.ShopperId}";
            var apiResult = await _apiClient.PutAsync(url, request);
            return apiResult;
        }

        private async Task<HttpResponseMessage> DeleteCartItems(Table table)
        {
            var inputs = table.CreateSet<DeleteCartItemForm>();
            var requests = CreateDeleteCartItemRequests(inputs.ToList());

            HttpResponseMessage lastApiResult = null;
            var url = $"api/shoppingcart/{_context.ShopperId}/items";
            foreach (var request in requests)
            {
                lastApiResult = await _apiClient.DeleteAsync(url, request);
                _context.ApiResults.Add(lastApiResult);
            }

            return lastApiResult;
        }

        private void CompareCartItems(List<ExpectedCartItem> expectedItems, List<CartItemInfo> lineItems)
        {
            expectedItems.Count().Should().Be(lineItems.Count());
            expectedItems.EqualTo(lineItems).Should().BeTrue();
        }

        private List<AddCartItemRequest> CreateAddCartItemRequests(List<AddToCartForm> inputs)
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

        private UpdateCartRequest CreateUpdateCartRequest(List<UpdateCartItemForm> inputs)
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

        private List<DeleteCartItemRequest> CreateDeleteCartItemRequests(List<DeleteCartItemForm> inputs)
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

        private static async Task<CartInfo> GetShoppingCart(HttpResponseMessage lastApiResult)
        {
            var resultStr = await lastApiResult.Content.ReadAsStringAsync();
            var shoppingCart = JsonConvert.DeserializeObject<CartInfo>(resultStr);
            return shoppingCart;
        }
    }
}
