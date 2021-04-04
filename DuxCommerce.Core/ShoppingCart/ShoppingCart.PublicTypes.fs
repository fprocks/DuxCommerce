namespace DuxCommerce.Core.ShoppingCarts.PublicTypes

open MongoDB.Bson.Serialization.Attributes
open MongoDB.Bson

type AddCartItemRequest = {
    ProductId: string
    Quantity: decimal
    }

type UpdateCartItemRequest = {
    ProductId: string
    Quantity: decimal
    }

type UpdateCartRequest = {
    CartItems: UpdateCartItemRequest seq
    }

type DeleteCartItemRequest = {
    ProductId: string
    }

[<CLIMutable>]
type CartItemDto = {
    ProductId: string
    ProductName: string
    Price: decimal
    Quantity: decimal
    ItemTotal: decimal
    }

[<CLIMutable>]
type ShoppingCartDto = {
    [<BsonId>]
    [<BsonRepresentation(BsonType.ObjectId)>]
    Id: string
    ShopperId: string
    LineItems: CartItemDto seq
    CartTotal: decimal
    }