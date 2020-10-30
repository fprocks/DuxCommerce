namespace DuxCommerce.ShoppingCarts

type AddCartItemRequest = {
    ProductId: int64
    Quantity: decimal
}

type UpdateCartItemRequest = {
    ProductId: int64
    Quantity: decimal
}

type UpdateCartRequest = {
    CartItems: UpdateCartItemRequest seq
}

type DeleteCartItemRequest = {
    ProductId: int64
}

[<CLIMutable>]
type CartItemInfo = {
    Id: int64
    CartId: int64
    ProductId: int64
    ProductName: string
    Price: decimal
    Quantity: decimal
    ItemTotal: decimal
}

[<CLIMutable>]
type CartInfo = {
    Id: int64
    ShopperId: int64
    LineItems: CartItemInfo seq
    CartTotal: decimal
}