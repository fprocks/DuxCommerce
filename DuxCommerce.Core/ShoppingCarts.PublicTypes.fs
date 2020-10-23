namespace DuxCommerce.ShoppingCarts

open DuxCommerce.Common

type AddCartItemRequest = {
    ProductId : int64
    Quantity: decimal
}

type CartInfo = {
    Id: int64
    ShopperId: int64
    CartTotal: decimal
}

type CartItemInfo = {
    Id: int64
    CartId: int64
    ProductName: string
    Price: decimal
    Quantity: decimal
    ItemTotal: decimal
}

type AddCartItem = int64 -> AddCartItemRequest -> Result<CartItemInfo, string>