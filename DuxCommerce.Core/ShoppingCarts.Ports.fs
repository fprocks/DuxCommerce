namespace DuxCommerce.ShoppingCarts.Ports

open DuxCommerce.Common
open DuxCommerce.ShoppingCarts.InternalTypes
open DuxCommerce.ShoppingCarts.SimpleTypes
open DuxCommerce.ShoppingCarts.PublicTypes

// Input port
type AddItemUseCase = int64 -> AddCartItemRequest -> Result<CartInfo, CustomError>
type UpdateCartUseCase = int64 -> UpdateCartRequest -> Result<CartInfo, CustomError>
type DeleteCartItemUseCase = int64 -> DeleteCartItemRequest -> Result<CartInfo, CustomError>

// Output port
type GetShoppingCart = ShopperId -> Result<CartInfo, CustomError>
type SaveCart = Cart -> Result<unit, CustomError>
