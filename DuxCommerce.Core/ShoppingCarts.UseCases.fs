namespace DuxCommerce.ShoppingCarts

open DuxCommerce.Catalogue.Dto
open DuxCommerce.Common
open DuxCommerce.ShoppingCarts.Dto
open DuxCommerce.ShoppingCarts.DomainTypes

type AddItemUseCase = AddCartItemRequest -> Result<unit, string>

module UseCases =

    let addCartItem
        getShopperCart
        getProduct
        addCartItem
        saveCartItem
        : AddItemUseCase =
        
        fun request ->
            result {
                let! validatedRequest = AddCartItemRequest.validate request
                let! cartInfo = getShopperCart (ShopperId.create 100L)
                let! productInfo = getProduct validatedRequest.ProductId
                let cart = ShoppingCart.toDomain cartInfo
                let! product = Product.toDomain productInfo
                let updatedCart = addCartItem cart product validatedRequest
                return! saveCartItem updatedCart
            }