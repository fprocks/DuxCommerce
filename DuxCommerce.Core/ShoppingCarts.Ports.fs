namespace DuxCommerce.ShoppingCarts

open DuxCommerce.Catalogue
open DuxCommerce.ShoppingCarts.DomainTypes
open DuxCommerce.ShoppingCarts.Dto

type GetShopperCart = ShopperId -> Result<CartInfo, string>
type GetProduct =  ProductId -> Result<ProductInfo, string>
type AddCartItem = Cart -> Product -> ValidatedAddItemRequest
type SaveCartItem = CartInfo -> Result<unit, string>