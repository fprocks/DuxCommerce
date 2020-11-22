namespace DuxCommerce.ShoppingCart.Dto

open DuxCommerce.Catalogue.SimpleTypes
open DuxCommerce.Common
open DuxCommerce.ShoppingCarts.InternalTypes
open DuxCommerce.ShoppingCarts.PublicTypes
open DuxCommerce.ShoppingCarts.SimpleTypes


module CartItemDto = 

    let fromDomain cartItem :CartItemDto =
        {
            Id = CartItemId.value cartItem.CartItemId
            CartId = ShoppingCartId.value cartItem.CartId
            ProductId = ProductId.value cartItem.ProductId
            ProductName = String255.value cartItem.ProductName
            Price = SalePrice.value cartItem.Price
            Quantity = ItemQuantity.value cartItem.Quantity
            ItemTotal = ItemTotal.value cartItem.ItemTotal
        }
            
    let toDomain (itemDto:CartItemDto) :CartItem =
        {
            CartItemId = CartItemId.create itemDto.Id
            CartId = ShoppingCartId.create itemDto.CartId
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
    
    let toDomain cartDto :ShoppingCart =
        {
            ShoppingCartId = ShoppingCartId.create cartDto.Id
            ShopperId = ShopperId.create cartDto.ShopperId
            LineItems = cartDto.LineItems |> Seq.map CartItemDto.toDomain
            CartTotal = CartTotal.create cartDto.CartTotal
        }
        

