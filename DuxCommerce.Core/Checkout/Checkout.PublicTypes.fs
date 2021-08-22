namespace DuxCommerce.Core.Checkout.PublicTypes

open MongoDB.Bson.Serialization.Attributes
open MongoDB.Bson
open DuxCommerce.Core.Shared.PublicTypes

[<CLIMutable>]
type CustomerInformationRequest = {
    Email: string
    ShippingAddress: AddressDto
    }

[<CLIMutable>]
type ShippingMethodRequest = {
    ShippingMethodId: string
    }

[<CLIMutable>]
type PaymentMethodRequest = {
    PaymentMethodId: string
    SameAsShipping: bool
    BillingAddress: AddressDto 
    }

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
    ShopperId: string
    Email: string
    ShippingAddress: AddressDto // Todo: how to design ShippingAddress so it can be nullable
    ShippingMethodId: string
    SameAsShipping: bool
    BillingAddress: AddressDto
    PaymentMethodId: string
    }