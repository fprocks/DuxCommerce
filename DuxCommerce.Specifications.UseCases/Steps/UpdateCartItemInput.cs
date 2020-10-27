using System.Collections.Generic;

namespace DuxCommerce.Specifications.UseCases.Steps
{
    public class UpdateCartItemInput
    {
        public int Product { get; set; }
        public decimal Quantity { get; set; }
    }
}
