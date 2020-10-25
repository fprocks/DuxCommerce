namespace DuxCommerce.ShoppingCarts

open DuxCommerce.Common
open DuxCommerce.ShoppingCarts.Dto

type AddItemUseCase = AddCartItemRequest -> Result<CartItemInfo, string>

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
                let! shopperCart = getShopperCart
                let! product = getProduct validatedRequest.ProductId
                let updatedCart = addCartItem shopperCart product validatedRequest
                return! saveCartItem updatedCart
            }