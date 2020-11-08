namespace DuxCommerce.ShoppingCarts

open DuxCommerce.Common
open DuxCommerce.ShoppingCarts.InternalTypes
open DuxCommerce.ShoppingCarts.Ports
open DuxCommerce.Catalogue

module UseCases =

    let addCartItem connString :AddCartItemUseCase =        
        fun shopperId request ->
            result {
                let! cartInfo = CartRepo.getShoppingCart connString shopperId
                let! productInfo = ProductRepo.getProduct connString request.ProductId
                let! updatedCart = ShoppingCart.addCartItem cartInfo productInfo request // pure
                do! CartRepo.saveShoppingCart connString updatedCart
                return! CartRepo.getShoppingCart connString shopperId
            }

    let updateCart connString :UpdateCartUseCase =
        fun shopperId request ->
            result {
                let! cartInfo = CartRepo.getShoppingCart connString shopperId
                let! updatedCart = ShoppingCart.updateCart cartInfo request // pure
                do! CartRepo.saveShoppingCart connString updatedCart
                return! CartRepo.getShoppingCart connString shopperId
            }

    let deleteCartItem connString :DeleteCartItemUseCase =
        fun shopperId request ->
            result {
                let! cartInfo = CartRepo.getShoppingCart connString shopperId
                let! cartAndItems = ShoppingCart.deleteCartItem cartInfo request // pure
                do! CartRepo.deleteCartItem connString cartAndItems
                return! CartRepo.getShoppingCart connString shopperId
            }