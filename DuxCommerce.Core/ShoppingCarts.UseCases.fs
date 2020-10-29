namespace DuxCommerce.ShoppingCarts

open DuxCommerce.Catalogue.Dto
open DuxCommerce.Common
open DuxCommerce.ShoppingCarts.Dto
open DuxCommerce.ShoppingCarts.DomainTypes

type AddItemUseCase = AddCartItemRequest -> Result<CartInfo, string>
type UpdateCartUseCase = UpdateCartRequest -> Result<CartInfo, string>

module UseCases =
    let addCartItem
        getShopperCart
        getProduct
        saveCart
        shopperId
        :AddItemUseCase =
        
        fun request ->
            result {
                let! cmd = AddCartItemRequest.validate request

                let! cartInfo = getShopperCart (ShopperId.create shopperId)
                let cart = ShoppingCart.toDomain cartInfo

                let! productInfo = getProduct cmd.ProductId
                let! product = ProductInfo.toDomain productInfo

                // Question: should this be passed in as dependency?
                let updatedCart = ShoppingCart.addCartItem cart product cmd
                do! saveCart updatedCart
                return! getShopperCart updatedCart.ShopperId
            }

    let updateCart
        getShopperCart
        saveCart
        shopperId
        :UpdateCartUseCase =

        fun request ->
            result {
                let! cmd = UpdateCartRequest.validate request

                let! cartInfo = getShopperCart (ShopperId.create shopperId)
                let cart = ShoppingCart.toDomain cartInfo

                let updatedCart = ShoppingCart.updateCart cart cmd
                do! saveCart updatedCart
                return! getShopperCart updatedCart.ShopperId
            }