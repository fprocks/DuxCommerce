namespace DuxCommerce.ShoppingCarts.Ports

open DuxCommerce.Common
open DuxCommerce.ShoppingCarts.PublicTypes

// Input port
type AddCartItemUseCase = string -> AddCartItemRequest -> ConfigReader<Result<ShoppingCartDto, CustomError>>
type UpdateCartUseCase = string -> UpdateCartRequest -> ConfigReader<Result<ShoppingCartDto, CustomError>>
type DeleteCartItemUseCase = string -> DeleteCartItemRequest -> ConfigReader<Result<ShoppingCartDto, CustomError>>

// Output port
type GetShoppingCart = string -> ConfigReader<Result<ShoppingCartDto, CustomError>>
type SaveShoppingCart = ShoppingCartDto -> ConfigReader<Result<unit, CustomError>>
type DeleteCartItem = ShoppingCartDto * (CartItemDto seq) -> ConfigReader<Result<uint, CustomError>>
