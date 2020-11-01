namespace DuxCommerce.ShoppingCarts.Ports

open DuxCommerce.ShoppingCarts.InternalTypes
open DuxCommerce.ShoppingCarts.SimpleTypes
open DuxCommerce.ShoppingCarts.PublicTypes

type GetShopperCart = ShopperId -> Result<CartInfo, string>
type SaveCart = Cart -> Result<unit, string>
type DeleteCartItem = Cart * CartItem seq -> Result<uint, string>