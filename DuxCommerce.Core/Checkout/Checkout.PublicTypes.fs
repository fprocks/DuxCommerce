namespace DuxCommerce.Core.Checkout.PublicTypes

open MongoDB.Bson.Serialization.Attributes
open MongoDB.Bson
open DuxCommerce.Core.Shared.PublicTypes

[<CLIMutable>]
type CustomerDto = {
    [<BsonId>]
    [<BsonRepresentation(BsonType.ObjectId)>]
    Id: string
    Username: string
    Password: string
    Email: string
    PhoneNumber: string
    FirstName: string
    LastName: string
    UserId: string
}

[<CLIMutable>]
type CheckoutDto = {
    [<BsonId>]
    [<BsonRepresentation(BsonType.ObjectId)>]
    Id: string
    ShoppingCartId: string
    Email: string
    ShippingAddress: AddressDto
    SameAsBilling: bool
    BillingAddress: AddressDto
    ShippingMethodId: string
    PaymentMethodId: string
}