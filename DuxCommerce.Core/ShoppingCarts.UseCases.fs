namespace DuxCommerce.ShoppingCarts

open DuxCommerce.Catalogue.Dto
open DuxCommerce.Common
open DuxCommerce.ShoppingCart.Dto
open DuxCommerce.ShoppingCarts.Commands
open DuxCommerce.ShoppingCarts.InternalTypes
open DuxCommerce.ShoppingCarts.Ports
open DuxCommerce.Catalogue

module UseCases =

    let addCartItem :AddCartItemUseCase =        
        fun shopperId request ->
            readerResult {
                let! cartDto = CartRepo.getShoppingCart shopperId
                let! productDto = ProductRepo.getProduct request.ProductId
                
                // Edge of domain
                let! cmd = AddCartItemCommand.fromRequest request |> ConfigReader.retn
                let cart = ShoppingCartDto.toDomain cartDto
                let! product = ProductDto.toDomain productDto |> ConfigReader.retn
                
                // Core of domain
                let updatedCart = ShoppingCart.addCartItem cart product cmd
                
                // Edge logic
                let cartDto = ShoppingCartDto.fromDomain updatedCart
            
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
                
                let (updatedCart, deletedItems) = ShoppingCart.deleteCartItem cart cmd
                
                let updatedCart = updatedCart |> ShoppingCartDto.fromDomain
                let deletedItems = deletedItems |> Seq.map CartItemDto.fromDomain
                
                do! CartRepo.deleteCartItem (updatedCart, deletedItems)
                return! CartRepo.getShoppingCart shopperId
            }