namespace DuxCommerce.ShoppingCarts

open DuxCommerce.Catalogue
open DuxCommerce.ShoppingCarts.DomainTypes

type GetShopperCart = int64 -> Result<CartInfo, string>
type GetProduct =  int64 -> Result<ProductInfo, string>
type AddCartItem = Cart -> Product -> ValidatedAddItemRequest
type SaveCartItem = CartInfo -> Result<unit, string>