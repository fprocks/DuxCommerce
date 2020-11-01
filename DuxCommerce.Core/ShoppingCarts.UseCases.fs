namespace DuxCommerce.ShoppingCarts

open DuxCommerce.Catalogue.Dto
open DuxCommerce.Common
open DuxCommerce.ShoppingCarts.Dto
open DuxCommerce.ShoppingCarts.InternalTypes
open DuxCommerce.ShoppingCarts.SimpleTypes
open DuxCommerce.ShoppingCarts.PublicTypes

type AddItemUseCase = int64 -> AddCartItemRequest -> Result<CartInfo, string>
type UpdateCartUseCase = int64 -> UpdateCartRequest -> Result<CartInfo, string>
type DeleteCartItemUseCase = int64 -> DeleteCartItemRequest -> Result<CartInfo, string>

module UseCases =
    let addCartItem
        getShopperCart
        getProduct
        saveCart        
        :AddItemUseCase =        
        fun shopperId request ->
            result {
                let! cmd = AddCartItemRequest.toCommand request
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
        :UpdateCartUseCase =
        fun shopperId request ->
            result {
                let! cmd = UpdateCartRequest.toCommand request
                let! cartInfo = getShopperCart (ShopperId.create shopperId)
                let cart = ShoppingCart.toDomain cartInfo
                
                // Question: should this be passed in as dependency?
                let updatedCart = ShoppingCart.updateCart cart cmd
                do! saveCart updatedCart
                return! getShopperCart updatedCart.ShopperId
            }

    let deleteCartItem
        getShopperCart
        deleteCartItem        
        :DeleteCartItemUseCase =
        fun shopperId request ->
            result {
                let! cmd = DeleteCartItemRequest.toCommand request
                let! cartInfo = getShopperCart (ShopperId.create shopperId)
                let cart = ShoppingCart.toDomain cartInfo

                // Question: should this be passed in as dependency?                                
                let carts = ShoppingCart.deleteCartItem cart cmd
                do! deleteCartItem carts
                return! getShopperCart cart.ShopperId
            }