using DuxCommerce.ShoppingCarts;
using Microsoft.FSharp.Collections;
using System.Collections.Generic;

namespace DuxCommerce.Specifications.UseCases.Steps
{
    public static class CartItemExtensions
    {
        public static bool EqualTo(this List<ExpectedCartItem> expectedItems, FSharpList<CartItemInfo> actualItems)
        {
            for (var index = 0; index < expectedItems.Count; index ++)
            {
                if (!expectedItems[index].EqualTo(actualItems[index]))
                    return false;
            }

            return true;
        }

        private static bool EqualTo(this ExpectedCartItem expected, CartItemInfo actual)
        {
            return expected.ProductId == actual.ProductId &&
                expected.Name == actual.ProductName &&
                expected.Price == actual.Price &&
                expected.Quantity == actual.Quantity &&
                expected.ItemTotal == actual.ItemTotal;
        }
    }
}
