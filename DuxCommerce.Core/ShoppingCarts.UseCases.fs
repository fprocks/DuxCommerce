namespace DuxCommerce.ShoppingCarts

open DuxCommerce.Common
module UseCases =

    let addCartItem
        getShopperCart
        getProduct
        recalculateCart
        insertCartItem
        : AddCartItem =
        
        fun addCartItemRequest ->
            result {
                let! shopperCart = getShopperCart
                let! product = getProduct addCartItemRequest.ProductId
                let updatedCart = recalculateCart shopperCart addCartItemRequest product
                return! insertCartItem updatedCart
            }