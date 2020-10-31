namespace DuxCommerce.ShoppingCarts.Ports

open DuxCommerce.Catalogue
open DuxCommerce.ShoppingCarts.InternalTypes
open DuxCommerce.ShoppingCarts.SimpleTypes
open DuxCommerce.ShoppingCarts.PublicTypes

type GetProduct =  ProductId -> Result<ProductInfo, string>
type GetShopperCart = ShopperId -> Result<CartInfo, string>
type SaveCart = Cart -> Result<unit, string>
type DeleteCartItem = Cart * CartItem seq -> Result<uint, string>