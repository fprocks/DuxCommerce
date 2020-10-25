namespace DuxCommerce.ShoppingCarts

open DuxCommerce.Catalogue

type AddCartItemRequest = {
    ProductId : int64
    Quantity: decimal
}

type ValidatedAddItemRequest = {
    ProductId : ProductId
    Quantity: ItemQuantity
}

module AddCartItemRequest =
    let validate (request:AddCartItemRequest) :Result<ValidatedAddItemRequest, string> =
        Ok {
            ProductId = ProductId.create request.ProductId
            Quantity = ItemQuantity.create request.Quantity
            }

type CartItemInfo = {
    Id: int64
    CartId: int64
    ProductId: int64
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