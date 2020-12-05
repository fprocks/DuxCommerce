namespace DuxCommerce.ShoppingCarts.PublicTypes

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
type CartItemDto = {
    Id: int64
    CartId: int64
    ProductId: int64
    ProductName: string
    Price: decimal
    Quantity: decimal
    ItemTotal: decimal
}

[<CLIMutable>]
type ShoppingCartDto = {
    Id: int64
    ShopperId: int64
    LineItems: CartItemDto seq
    CartTotal: decimal
}