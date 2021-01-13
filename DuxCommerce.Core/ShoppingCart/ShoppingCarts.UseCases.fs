namespace DuxCommerce.ShoppingCarts

open DuxCommerce.Catalogue.Dto
open DuxCommerce.Common
open DuxCommerce.ShoppingCart.Dto
open DuxCommerce.ShoppingCarts.Commands
open DuxCommerce.ShoppingCarts.InternalTypes
open DuxCommerce.ShoppingCarts.MongoRepos
open DuxCommerce.Catalogue.MongoRepos
open DuxCommerce.ShoppingCarts.PublicTypes

type AddCartItemUseCase = string -> AddCartItemRequest -> ConfigReader<Result<ShoppingCartDto, CustomError>>
type UpdateCartUseCase = string -> UpdateCartRequest -> ConfigReader<Result<ShoppingCartDto, CustomError>>
type DeleteCartItemUseCase = string -> DeleteCartItemRequest -> ConfigReader<Result<ShoppingCartDto, CustomError>>

module ShoppingCartUseCases =

    let addCartItem :AddCartItemUseCase =        
        fun shopperId request ->
            readerResult {
                // Imperative shell
                let! cartDto = CartRepo.getShoppingCart shopperId
                let! productDto = ProductRepo.getProduct request.ProductId
                
                // Non-domain functional code
                let! cmd = AddCartItemCommand.fromRequest request |> ConfigReader.retn
                let cart = ShoppingCartDto.toDomain cartDto
                let! product = ProductDto.toDomain productDto |> ConfigReader.retn
                
                // Functional core domain
                let updatedCart = ShoppingCart.addCartItem cart product cmd
                
                // Non-domain functional code
                let cartDto = ShoppingCartDto.fromDomain updatedCart
            
                // Imperative shell
                do! CartRepo.saveShoppingCart cartDto
                return! CartRepo.getShoppingCart shopperId
            }

    let updateCart :UpdateCartUseCase =
        fun shopperId request -> 
            readerResult {
                let! cartDto = CartRepo.getShoppingCart shopperId
                
                let! cmd = UpdateCartCommand.fromRequest request |> ConfigReader.retn
                let cart = ShoppingCartDto.toDomain cartDto
            
                let updatedCart = ShoppingCart.updateCart cart cmd
                                  |> ShoppingCartDto.fromDomain

                do! CartRepo.saveShoppingCart updatedCart
                return! CartRepo.getShoppingCart shopperId
            }

    let deleteCartItem :DeleteCartItemUseCase =
        fun shopperId request ->
            readerResult {
                let! cartDto = CartRepo.getShoppingCart shopperId
                
                let! cmd = DeleteCartItemCommand.fromRequest request |> ConfigReader.retn
                let cart = ShoppingCartDto.toDomain cartDto
                
                let updatedCart = ShoppingCart.deleteCartItem cart cmd
                
                let updatedCart = updatedCart |> ShoppingCartDto.fromDomain
                
                do! CartRepo.saveShoppingCart updatedCart
                return! CartRepo.getShoppingCart shopperId
            }