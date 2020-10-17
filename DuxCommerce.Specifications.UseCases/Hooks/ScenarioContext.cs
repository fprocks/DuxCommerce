using DuxCommerce.Catalogue;
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
        }

        public List<ProductInfo> ProductRequests { get; set; }
        
        public HttpResponseMessage ApiResult { get; set; }
    }
}
