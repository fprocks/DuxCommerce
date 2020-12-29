using System;
using System.Collections.Generic;
using System.Text;

namespace DuxCommerce.Specifications.UseCases.Model
{
    public class ExpectedCartItem
    {
        public int Product { get; set; }
        public long ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal ItemTotal { get; set; }
    }
}
