using DuxCommerce.Catalogue;
using System;
using System.Collections.Generic;

namespace DuxCommerce.Specifications.UseCases.Hooks
{
    public class MyScenarioContext
    {
        public MyScenarioContext()
        {
            ProductInfoList = new List<ProductInfo>();
            UserId = new Random().Next(1, 1000000000);
        }

        public List<ProductInfo> ProductInfoList { get; set; }

        public int UserId { get; set; }
    }
}
