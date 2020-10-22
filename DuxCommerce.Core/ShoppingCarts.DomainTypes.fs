namespace DuxCommerce.ShoppingCarts

open DuxCommerce.Catalogue
open DuxCommerce.Common

type Cart = {
    Id : CartId
    ShopperId : ShopperId
    CartTotal: CartTotal
}

type CartItem = {
    Id: CartItemId
    CartId: CartId
    ProductName: String255
    Price: SalePrice
    Quantity: ItemQuantity
    ItemTotal: ItemTotal
}