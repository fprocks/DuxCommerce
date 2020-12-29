using DuxCommerce.ShoppingCarts.PublicTypes;
using DuxCommerce.Specifications.UseCases.Model;
using System.Collections.Generic;

namespace DuxCommerce.Specifications.UseCases.Extensions
{
    public static class CartItemExtensions
    {
        public static bool EqualTo(this List<CartItem> expectedItems, List<CartItemDto> actualItems)
        {
            for (var index = 0; index < expectedItems.Count; index++)
            {
                if (!expectedItems[index].EqualTo(actualItems[index]))
                    return false;
            }

            return true;
        }

        private static bool EqualTo(this CartItem expected, CartItemDto actual)
        {
            return expected.ProductId == actual.ProductId &&
                expected.Name == actual.ProductName &&
                expected.Price == actual.Price &&
                expected.Quantity == actual.Quantity &&
                expected.ItemTotal == actual.ItemTotal;
        }
    }
}
