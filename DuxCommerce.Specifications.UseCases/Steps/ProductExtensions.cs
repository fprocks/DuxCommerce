using DuxCommerce.Catalogue;
using System.Collections.Generic;

namespace DuxCommerce.Specifications.UseCases.Steps
{
    public static class ProductExtensions
    {
        public static bool EqualTo(this List<ProductInfo> actual, List<ProductInfo> expected)
        {
            for (var index = 0; index < actual.Count; index++)
            {
                if (!actual[index].EqualTo(expected[index]))
                    return false;
            }

            return true;
        }

        public static bool EqualTo(this ProductInfo product1, ProductInfo product2)
        {
            return product1.Name == product2.Name &&
                product1.Description == product2.Description &&
                product1.Price == product2.Price &&
                product1.Retail == product2.Retail &&
                product1.Cost == product2.Cost &&
                product1.Length == product2.Length &&
                product1.Width == product2.Width &&
                product1.Height == product2.Height &&
                product1.Weight == product2.Weight &&
                product1.ShippingType == product2.ShippingType &&
                product1.SKU == product2.SKU &&
                product1.Barcode == product2.Barcode &&
                product1.TrackInventory == product2.TrackInventory &&
                product1.OutOfStockRule == product2.OutOfStockRule;
        }
    }
}
