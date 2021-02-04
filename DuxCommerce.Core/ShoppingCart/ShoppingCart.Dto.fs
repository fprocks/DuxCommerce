namespace DuxCommerce.Core.ShoppingCart.Dto

open DuxCommerce.Core.Catalogue.SimpleTypes
open DuxCommerce.Common
open DuxCommerce.Core.ShoppingCarts.InternalTypes
open DuxCommerce.Core.ShoppingCarts.PublicTypes
open DuxCommerce.Core.ShoppingCarts.SimpleTypes

module CartItemDto = 

    let fromDomain (cartItem:CartItem) :CartItemDto =
        {
            ProductId = ProductId.value cartItem.ProductId
            ProductName = String255.value cartItem.ProductName
            Price = SalePrice.value cartItem.Price
            Quantity = ItemQuantity.value cartItem.Quantity
            ItemTotal = ItemTotal.value cartItem.ItemTotal
        }
            
    let toDomain (itemDto:CartItemDto) :CartItem =
        {
            ProductId = ProductId.create itemDto.ProductId
            ProductName = String255 itemDto.ProductName
            Price = SalePrice.create itemDto.Price
            Quantity = ItemQuantity.create itemDto.Quantity
            ItemTotal = ItemTotal.create itemDto.ItemTotal
        }

module ShoppingCartDto =

    let fromDomain cart :ShoppingCartDto =
        {
            Id = ShoppingCartId.value cart.ShoppingCartId
            ShopperId = ShopperId.value cart.ShopperId
            LineItems = cart.LineItems |> Seq.map CartItemDto.fromDomain
            CartTotal = CartTotal.value cart.CartTotal
        }
    
    let toDomain (cartDto:ShoppingCartDto) :ShoppingCart =
        {
            ShoppingCartId = ShoppingCartId.create cartDto.Id
            ShopperId = ShopperId.create cartDto.ShopperId
            LineItems = cartDto.LineItems |> Seq.map CartItemDto.toDomain
            CartTotal = CartTotal.create cartDto.CartTotal
        }
        

