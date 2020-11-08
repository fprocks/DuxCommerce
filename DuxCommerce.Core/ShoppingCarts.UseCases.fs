namespace DuxCommerce.ShoppingCarts

open DuxCommerce.Common
open DuxCommerce.ShoppingCarts.InternalTypes
open DuxCommerce.ShoppingCarts.Ports

module UseCases =

    let addCartItem getShopperCart getProduct saveCart :AddCartItemUseCase =        
        fun shopperId request ->
            result {
                let! cartInfo = getShopperCart shopperId
                let! productInfo = getProduct request.ProductId
                let! updatedCart = ShoppingCart.addCartItem cartInfo productInfo request // pure
                do! saveCart updatedCart
                return! getShopperCart shopperId
            }

    let updateCart getShopperCart saveCart :UpdateCartUseCase =
        fun shopperId request ->
            result {
                let! cartInfo = getShopperCart shopperId
                let! updatedCart = ShoppingCart.updateCart cartInfo request // pure
                do! saveCart updatedCart
                return! getShopperCart shopperId
            }

    let deleteCartItem getShopperCart deleteCartItem :DeleteCartItemUseCase =
        fun shopperId request ->
            result {
                let! cartInfo = getShopperCart shopperId
                let! cartAndItems = ShoppingCart.deleteCartItem cartInfo request // pure
                do! deleteCartItem cartAndItems
                return! getShopperCart shopperId
            }