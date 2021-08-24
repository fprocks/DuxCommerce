using System.Collections.Generic;
using DuxCommerce.Core.ShoppingCarts.PublicTypes;
using DuxCommerce.Specifications.Models;

namespace DuxCommerce.Specifications.Extensions
{
    public static class CartItemExtensions
    {
        public static bool EqualTo(this List<ShoppingCartItem> expectedItems, List<CartItemDto> actualItems)
        {
            for (var index = 0; index < expectedItems.Count; index++)
                if (!expectedItems[index].EqualTo(actualItems[index]))
                    return false;

            return true;
        }

        private static bool EqualTo(this ShoppingCartItem expected, CartItemDto actual)
        {
            return expected.ProductId == actual.ProductId &&
                   expected.Name == actual.ProductName &&
                   expected.Price == actual.Price &&
                   expected.Quantity == actual.Quantity &&
                   expected.ItemTotal == actual.ItemTotal;
        }
    }
}