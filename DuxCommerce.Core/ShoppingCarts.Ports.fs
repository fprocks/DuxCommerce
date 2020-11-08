namespace DuxCommerce.ShoppingCarts.Ports

open DuxCommerce.Common
open DuxCommerce.ShoppingCarts.PublicTypes

// Input port
type AddCartItemUseCase = int64 -> AddCartItemRequest -> Result<ShoppingCartDto, CustomError>
type UpdateCartUseCase = int64 -> UpdateCartRequest -> Result<ShoppingCartDto, CustomError>
type DeleteCartItemUseCase = int64 -> DeleteCartItemRequest -> Result<ShoppingCartDto, CustomError>

// Output port
type GetShoppingCart = int64 -> Result<ShoppingCartDto, CustomError>
type SaveShoppingCart = ShoppingCartDto -> Result<unit, CustomError>
type DeleteCartItem = ShoppingCartDto * (CartItemDto seq) -> Result<uint, CustomError>
