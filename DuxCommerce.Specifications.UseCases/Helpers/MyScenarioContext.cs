using DuxCommerce.Catalogue;
using System;
using System.Collections.Generic;
using System.Text;

namespace DuxCommerce.Specifications.UseCases.Helpers
{
    public class MyScenarioContext
    {

        public MyScenarioContext()
        {
            ProductInfoList = new List<ProductInfo>();
        }

        public List<ProductInfo> ProductInfoList { get; set; }
    }
}
