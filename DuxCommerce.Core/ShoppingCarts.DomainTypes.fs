namespace DuxCommerce.ShoppingCarts

open DuxCommerce.Catalogue
open DuxCommerce.Common

type CartItem = {
    Id: CartItemId
    CartId: CartId
    ProductName: String255
    Price: SalePrice
    Quantity: ItemQuantity
    ItemTotal: ItemTotal
}

type Cart = {
    Id : CartId
    ShopperId : ShopperId
    LineItems: CartItem list
    CartTotal: CartTotal
}