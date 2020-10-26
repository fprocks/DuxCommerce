namespace DuxCommerce.ShoppingCarts

open System.Reflection.Metadata
open DuxCommerce.Catalogue

type ShopperId = private ShopperId of int64
module ShopperId =
    let value (ShopperId id) = id
    let create id = ShopperId id
    
type CartId = private CartId of int64
module CartId =
    let value (CartId id) = id
    let create id = CartId id

type CartItemId = private CartItemId of int64
module CartItemId =
    let value (CartItemId id) = id
    let create id = CartItemId id

type ItemQuantity = private ItemQuantity of decimal
module ItemQuantity =
    let value (ItemQuantity quantity) = quantity
    let create quantity = ItemQuantity quantity
    let add (ItemQuantity quantity) (ItemQuantity qty) =
        let newQty = quantity + qty
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


