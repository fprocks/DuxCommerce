namespace DuxCommerce.ShoppingCarts

open DuxCommerce.Common
module UseCases =

    let addCartItem
        getShopperCart
        getProduct
        recalculateCart
        saveCart
        : AddCartItem =
        
        fun addCartItemRequest ->
            result {
                let! shopperCart = getShopperCart
                let! product = getProduct addCartItemRequest
                let updatedCart = recalculateCart shopperCart addCartItemRequest product
                return! saveCart updatedCart
            }