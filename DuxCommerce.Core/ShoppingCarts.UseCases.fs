namespace DuxCommerce.ShoppingCarts

open DuxCommerce.Catalogue.Dto
open DuxCommerce.Common
open DuxCommerce.ShoppingCarts.Dto
open DuxCommerce.ShoppingCarts.InternalTypes
open DuxCommerce.ShoppingCarts.Ports
open DuxCommerce.ShoppingCarts.SimpleTypes

module UseCases =

    let addCartItem getShopperCart getProduct saveCart :AddCartItemUseCase =        
        fun shopperId request ->
            result {
                let! cmd = AddCartItemRequest.toCommand request // pure
                let! cartInfo = getShopperCart (ShopperId.create shopperId)
                let cart = ShoppingCart.toDomain cartInfo // pure
                let! productInfo = getProduct cmd.ProductId
                let! product = ProductInfo.toDomain productInfo // pure

                // Todo: should this be passed in as dependency?
                let updatedCart = ShoppingCart.addCartItem cart product cmd // pure
                do! saveCart updatedCart
                return! getShopperCart updatedCart.ShopperId
            }

    let updateCart getShopperCart saveCart :UpdateCartUseCase =
        fun shopperId request ->
            result {
                let! cmd = UpdateCartRequest.toCommand request // pure
                let! cartInfo = getShopperCart (ShopperId.create shopperId)
                let cart = ShoppingCart.toDomain cartInfo // pure
                 
                // Todo: should this be passed in as dependency?
                let updatedCart = ShoppingCart.updateCart cart cmd // pure
                do! saveCart updatedCart
                return! getShopperCart updatedCart.ShopperId
            }

    let deleteCartItem getShopperCart deleteCartItem :DeleteCartItemUseCase =
        fun shopperId request ->
            result {
                let! cmd = DeleteCartItemRequest.toCommand request // pure
                let! cartInfo = getShopperCart (ShopperId.create shopperId)
                let cart = ShoppingCart.toDomain cartInfo // pure

                // Todo: should this be passed in as dependency?                                
                let cartAndItems = ShoppingCart.deleteCartItem cart cmd // pure
                do! deleteCartItem cartAndItems
                return! getShopperCart cart.ShopperId
            }