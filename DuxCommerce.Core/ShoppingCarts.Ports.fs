namespace DuxCommerce.ShoppingCarts.Ports

open DuxCommerce.Common
open DuxCommerce.ShoppingCarts.PublicTypes

// Input port
type AddCartItemUseCase = int64 -> AddCartItemRequest -> Result<CartInfo, CustomError>
type UpdateCartUseCase = int64 -> UpdateCartRequest -> Result<CartInfo, CustomError>
type DeleteCartItemUseCase = int64 -> DeleteCartItemRequest -> Result<CartInfo, CustomError>

// Output port
type GetShoppingCart = int64 -> Result<CartInfo, CustomError>
type SaveShoppingCart = CartInfo -> Result<unit, CustomError>
type DeleteCartItem = CartInfo * (CartItemInfo seq) -> Result<uint, CustomError>
