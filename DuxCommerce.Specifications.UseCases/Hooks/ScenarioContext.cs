using DuxCommerce.Catalogue.PublicTypes;
using DuxCommerce.Settings.PublicTypes;
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
            ApiResults = new List<HttpResponseMessage>();
            CreatedProducts = new List<ProductDto>();

            // Todo: generate shopperId from front end before we can read it from ShopperContext
            ShopperId = new Random().Next();
        }

        public List<HttpResponseMessage> ApiResults { get; set; }

        public List<ProductDto> CreatedProducts { get; set; }

        public StoreDetailsDto CreatedStoreDetails { get; set; }

        public long ShopperId { get; set; }
    }
}
