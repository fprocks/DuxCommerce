namespace DuxCommerce.ShoppingCarts.DomainTypes

open DuxCommerce.Catalogue
open DuxCommerce.Common
open DuxCommerce.ShoppingCarts

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