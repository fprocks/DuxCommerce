namespace DuxCommerce.ShoppingCarts

open DuxCommerce.Common
open DuxCommerce.ShoppingCarts.InternalTypes
open DuxCommerce.ShoppingCarts.Ports
open DuxCommerce.Catalogue

module UseCases =

    let addCartItem :AddCartItemUseCase =        
        fun shopperId request ->
            readerResult {
                let! cartDto = CartRepo.getShoppingCart shopperId
                let! productDto = ProductRepo.getProduct request.ProductId
                
                let! updatedCart = 
                    ShoppingCart.addCartItem cartDto productDto request 
                    |> ConfigReader.retn
                
                do! CartRepo.saveShoppingCart updatedCart
                return! CartRepo.getShoppingCart shopperId
            }

    let updateCart :UpdateCartUseCase =
        fun shopperId request -> 
            readerResult {
                let! cartDto = CartRepo.getShoppingCart shopperId
                
                let! updatedCart = 
                    ShoppingCart.updateCart cartDto request
                    |> ConfigReader.retn
                
                do! CartRepo.saveShoppingCart updatedCart
                return! CartRepo.getShoppingCart shopperId
            }

    let deleteCartItem :DeleteCartItemUseCase =
        fun shopperId request ->
            readerResult {
                let! cartDto = CartRepo.getShoppingCart shopperId
                
                let! cartAndItems = 
                    ShoppingCart.deleteCartItem cartDto request
                    |> ConfigReader.retn
                
                do! CartRepo.deleteCartItem cartAndItems
                return! CartRepo.getShoppingCart shopperId
            }