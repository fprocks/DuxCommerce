using DuxCommerce.Core.Catalogue.PublicTypes;
using System.Collections.Generic;

namespace DuxCommerce.Specifications.UseCases.Extensions
{
    public static class ProductExtensions
    {
        public static bool EqualTo(this ProductDto expected, ProductDto actual)
        {
            return expected.Name == actual.Name &&
                expected.Description == actual.Description &&
                expected.Price == actual.Price &&
                expected.Retail == actual.Retail &&
                expected.Cost == actual.Cost &&
                expected.Length == actual.Length &&
                expected.Width == actual.Width &&
                expected.Height == actual.Height &&
                expected.Weight == actual.Weight &&
                expected.ProductType == actual.ProductType &&
                expected.SKU == actual.SKU &&
                expected.Barcode == actual.Barcode &&
                expected.TrackInventory == actual.TrackInventory &&
                expected.OutOfStockRule == actual.OutOfStockRule;
        }
    }
}
