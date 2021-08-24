using System;
using System.Collections.Generic;
using System.Net.Http;
using DuxCommerce.Core.Catalogue.PublicTypes;

namespace DuxCommerce.Specifications.Hooks
{
    public class StepContext
    {
        public StepContext()
        {
            CreatedProducts = new List<ProductDto>();

            // Todo: generate shopperId from front end before we can read it from ShopperContext
            ShopperId = new Random().Next();
        }

        public HttpResponseMessage ApiResult { get; set; }

        public List<ProductDto> CreatedProducts { get; set; }

        public long ShopperId { get; set; }
    }
}