namespace DuxCommerce.Core.ShoppingCarts.SimpleTypes

open DuxCommerce.Core.Catalogue.SimpleTypes

type ShopperId = private ShopperId of string

module ShopperId =

    let value (ShopperId id) = id

    let create id = ShopperId id
    
type ShoppingCartId = private ShoppingCartId of string

module ShoppingCartId =

    let value (ShoppingCartId id) = id

    let create id = ShoppingCartId id


type ItemQuantity = private ItemQuantity of decimal

module ItemQuantity =

    let value (ItemQuantity quantity) = quantity

    let create quantity = ItemQuantity quantity

    let add (ItemQuantity qty1) (ItemQuantity qty2) =
        let newQty = qty1 + qty2
        create newQty

type ItemTotal = private ItemTotal of decimal

module ItemTotal =

    let value (ItemTotal total) = total

    let create total = ItemTotal total

    let calculate (SalePrice price) (ItemQuantity qty) =
        let total = price * qty
        create total    
    
type CartTotal = private CartTotal of decimal

module CartTotal =

    let value (CartTotal total) = total

    let create total = CartTotal total
    
    let addItemTotal (CartTotal cartTotal) (ItemTotal itemTotal) =
        let newTotal = cartTotal + itemTotal
        create newTotal


