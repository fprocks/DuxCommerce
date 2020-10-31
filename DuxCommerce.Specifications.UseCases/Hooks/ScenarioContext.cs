using DuxCommerce.Catalogue;
using DuxCommerce.Catalogue.PublicTypes;
using DuxCommerce.ShoppingCarts.PublicTypes;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace DuxCommerce.Specifications.UseCases.Hooks
{
    public class ScenarioContext
    {
        public ScenarioContext()
        {
            ProductRequests = new List<ProductInfo>();
            ApiResults = new List<HttpResponseMessage>();
            CreatedProducts = new List<ProductInfo>();

            // Todo: generate shopperId from front end before we can read it from ShopperContext
            ShopperId = new Random().Next();
        }

        public List<ProductInfo> ProductRequests { get; set; }

        public List<HttpResponseMessage> ApiResults { get; set; }

        public List<ProductInfo> CreatedProducts { get; set; }

        public CartInfo ShoppingCart { get; set; }

        public long ShopperId { get; set; }
    }
}
