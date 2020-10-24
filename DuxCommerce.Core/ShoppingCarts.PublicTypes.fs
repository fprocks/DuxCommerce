namespace DuxCommerce.ShoppingCarts

type AddCartItemRequest = {
    ProductId : int64
    Quantity: decimal
}

type CartItemInfo = {
    Id: int64
    CartId: int64
    ProductName: string
    Price: decimal
    Quantity: decimal
    ItemTotal: decimal
}

type CartInfo = {
    Id: int64
    ShopperId: int64
    LineItems: CartItemInfo list
    CartTotal: decimal
}

type AddCartItem = AddCartItemRequest -> Result<CartItemInfo, string>