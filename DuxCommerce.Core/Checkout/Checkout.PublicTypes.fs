namespace DuxCommerce.Core.Checkout.PublicTypes

open MongoDB.Bson.Serialization.Attributes
open MongoDB.Bson
open DuxCommerce.Core.Shared.PublicTypes

[<CLIMutable>]
type CustomerDto = {
    [<BsonId>]
    [<BsonRepresentation(BsonType.ObjectId)>]
    Id: string
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
    Email: string
    ShippingAddressId: string
    [<BsonIgnore>]
    ShippingAddress: AddressDto
    SameAsBilling: bool
    BillingAddressId: string
    [<BsonIgnore>]
    BillingAddress: AddressDto
    ShippingMethodId: string
    PaymentMethodId: string
    ShopperId: string
}