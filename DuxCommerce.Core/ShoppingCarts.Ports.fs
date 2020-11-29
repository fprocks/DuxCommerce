namespace DuxCommerce.ShoppingCarts.Ports

open DuxCommerce.Common
open DuxCommerce.ShoppingCarts.PublicTypes

// Input port
type AddCartItemUseCase = int64 -> AddCartItemRequest -> ConfigReader<Result<ShoppingCartDto, CustomError>>
type UpdateCartUseCase = int64 -> UpdateCartRequest -> ConfigReader<Result<ShoppingCartDto, CustomError>>
type DeleteCartItemUseCase = int64 -> DeleteCartItemRequest -> ConfigReader<Result<ShoppingCartDto, CustomError>>

// Output port
type GetShoppingCart = int64 -> ConfigReader<Result<ShoppingCartDto, CustomError>>
type SaveShoppingCart = ShoppingCartDto -> ConfigReader<Result<unit, CustomError>>
type DeleteCartItem = ShoppingCartDto * (CartItemDto seq) -> ConfigReader<Result<uint, CustomError>>
