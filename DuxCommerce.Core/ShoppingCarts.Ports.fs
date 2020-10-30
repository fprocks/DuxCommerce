namespace DuxCommerce.ShoppingCarts

open DuxCommerce.Catalogue
open DuxCommerce.ShoppingCarts.DomainTypes

type GetProduct =  ProductId -> Result<ProductInfo, string>
type GetShopperCart = ShopperId -> Result<CartInfo, string>
type SaveCart = Cart -> Result<unit, string>
type DeleteCartItem = Cart * CartItem seq -> Result<uint, string>