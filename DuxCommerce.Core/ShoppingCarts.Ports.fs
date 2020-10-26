namespace DuxCommerce.ShoppingCarts

open DuxCommerce.Catalogue
open DuxCommerce.ShoppingCarts.DomainTypes

type GetShopperCart = Result<CartInfo, string>
type GetProduct =  ProductId -> Result<ProductInfo, string>
type SaveCartItem = Cart -> Result<unit, string>