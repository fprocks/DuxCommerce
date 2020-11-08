namespace DuxCommerce.ShoppingCarts.Dto

open DuxCommerce.ShoppingCarts.PublicTypes
open DuxCommerce.ShoppingCarts.InternalTypes
open DuxCommerce.ShoppingCarts.SimpleTypes
open DuxCommerce.Catalogue.SimpleTypes
open DuxCommerce.Common

module CartItemDto = 

    let fromDomain (cartItem:CartItem) :CartItemDto =
        {
            Id = CartItemId.value cartItem.Id
            CartId = CartId.value cartItem.CartId
            ProductId = ProductId.value cartItem.ProductId
            ProductName = String255.value cartItem.ProductName
            Price = SalePrice.value cartItem.Price
            Quantity = ItemQuantity.value cartItem.Quantity
            ItemTotal = ItemTotal.value cartItem.ItemTotal
        }
            
    let toDomain (cartItemDto:CartItemDto) :CartItem =
        {
            Id = CartItemId.create cartItemDto.Id
            CartId = CartId.create cartItemDto.CartId
            ProductId = ProductId.create cartItemDto.ProductId
            ProductName = String255 cartItemDto.ProductName
            Price = SalePrice.create cartItemDto.Price
            Quantity = ItemQuantity.create cartItemDto.Quantity
            ItemTotal = ItemTotal.create cartItemDto.ItemTotal
        }

module CartDto =

    let fromDomain (cart:ShoppingCart) :ShoppingCartDto =
        {
            Id = CartId.value cart.Id
            ShopperId = ShopperId.value cart.ShopperId
            LineItems = cart.LineItems |> Seq.map CartItemDto.fromDomain
            CartTotal = CartTotal.value cart.CartTotal
        }
    
    let toDomain (cartDto:ShoppingCartDto) :ShoppingCart =
        {
            Id = CartId.create cartDto.Id
            ShopperId = ShopperId.create cartDto.ShopperId
            LineItems = cartDto.LineItems |> Seq.map CartItemDto.toDomain
            CartTotal = CartTotal.create cartDto.CartTotal
        }