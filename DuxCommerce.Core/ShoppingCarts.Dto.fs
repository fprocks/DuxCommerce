namespace DuxCommerce.ShoppingCarts.Dto

open DuxCommerce.Catalogue
open DuxCommerce.Common
open DuxCommerce.ShoppingCarts.InternalTypes
open DuxCommerce.ShoppingCarts.SimpleTypes
open DuxCommerce.ShoppingCarts.PublicTypes
open DuxCommerce.Catalogue.SimpleTypes

module AddCartItemRequest =
    let toCommand (request:AddCartItemRequest) :Result<AddCartItemCmd, CustomError> =
        Ok {
            ProductId = ProductId.create request.ProductId
            Quantity = ItemQuantity.create request.Quantity
        }

module UpdateCartRequest =
    let toCommand (request:UpdateCartRequest) :Result<UpdateCartCmd, CustomError> =
        let createItemCmd (request:UpdateCartItemRequest) :UpdateCartItemCmd=
            {
                ProductId = ProductId.create request.ProductId
                Quantity = ItemQuantity.create request.Quantity
            }
        Ok {
            UpdateItems = request.CartItems |> Seq.map createItemCmd
            }

module DeleteCartItemRequest =
    let toCommand (request:DeleteCartItemRequest) :Result<DeleteCartItemCmd, CustomError> =
        Ok {
            ProductId = ProductId.create request.ProductId
        }
        
module CartItem =        
    let fromDomain (cartItem:CartItem) :CartItemInfo =
        {
            Id = CartItemId.value cartItem.Id
            CartId = CartId.value cartItem.CartId
            ProductId = ProductId.value cartItem.ProductId
            ProductName = String255.value cartItem.ProductName
            Price = SalePrice.value cartItem.Price
            Quantity = ItemQuantity.value cartItem.Quantity
            ItemTotal = ItemTotal.value cartItem.ItemTotal
        }
            
    let toDomain (itemInfo:CartItemInfo) :CartItem =
        {
            Id = CartItemId.create itemInfo.Id
            CartId = CartId.create itemInfo.CartId
            ProductId = ProductId.create itemInfo.ProductId
            ProductName = String255 itemInfo.ProductName
            Price = SalePrice.create itemInfo.Price
            Quantity = ItemQuantity.create itemInfo.Quantity
            ItemTotal = ItemTotal.create itemInfo.ItemTotal
        }

module ShoppingCart =                
    let fromDomain (cart:Cart) :CartInfo =
        {
            Id = CartId.value cart.Id
            ShopperId = ShopperId.value cart.ShopperId
            LineItems = cart.LineItems |> Seq.map CartItem.fromDomain
            CartTotal = CartTotal.value cart.CartTotal
        }
        
    let toDomain (cartInfo:CartInfo) :Cart =
        {
            Id = CartId.create cartInfo.Id
            ShopperId = ShopperId.create cartInfo.ShopperId
            LineItems = cartInfo.LineItems |> Seq.map CartItem.toDomain
            CartTotal = CartTotal.create cartInfo.CartTotal
        }