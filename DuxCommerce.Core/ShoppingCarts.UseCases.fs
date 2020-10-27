namespace DuxCommerce.ShoppingCarts

open DuxCommerce.Catalogue.Dto
open DuxCommerce.Common
open DuxCommerce.ShoppingCarts.Dto
open DuxCommerce.ShoppingCarts.DomainTypes

type AddItemUseCase = AddCartItemRequest -> Result<CartInfo, string>

module UseCases =
    let addCartItem
        getShopperCart
        getProduct
        addCartItem
        saveCartItem
        :AddItemUseCase =
        
        fun request ->
            result {
                let! validatedRequest = AddCartItemRequest.validate request
                let! cartInfo = getShopperCart (ShopperId.create 100L)
                let! productInfo = getProduct validatedRequest.ProductId
                let cart = ShoppingCart.toDomain cartInfo
                let! product = ProductInfo.toDomain productInfo
                let updatedCart = addCartItem cart product validatedRequest
                do! saveCartItem updatedCart
                return! getShopperCart updatedCart.ShopperId
            }