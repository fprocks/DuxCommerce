namespace DuxCommerce.ShoppingCarts

open DuxCommerce.Catalogue
open DuxCommerce.ShoppingCarts.DomainTypes

type GetShopperCart = ShopperId -> Result<CartInfo, string>
type GetProduct =  ProductId -> Result<ProductInfo, string>
type SaveCart = Cart -> Result<unit, string>