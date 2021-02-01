namespace DuxCommerce.Core.ShoppingCarts

open DuxCommerce.Core.Catalogue.Dto
open DuxCommerce.Common
open DuxCommerce.Core.ShoppingCart.Dto
open DuxCommerce.Core.ShoppingCarts.Commands
open DuxCommerce.Core.ShoppingCarts.InternalTypes
open DuxCommerce.Core.ShoppingCarts.MongoRepos
open DuxCommerce.Core.Catalogue.MongoRepos
open DuxCommerce.Core.ShoppingCarts.PublicTypes

module ShoppingCartUseCases =

    type AddCartItemUseCase = string -> AddCartItemRequest -> ConfigReader<Result<ShoppingCartDto, CustomError>>
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

    type UpdateCartUseCase = string -> UpdateCartRequest -> ConfigReader<Result<ShoppingCartDto, CustomError>>
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

    type DeleteCartItemUseCase = string -> DeleteCartItemRequest -> ConfigReader<Result<ShoppingCartDto, CustomError>>
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