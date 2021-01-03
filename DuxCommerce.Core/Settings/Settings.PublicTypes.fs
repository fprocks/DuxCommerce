namespace DuxCommerce.Settings.PublicTypes

open MongoDB.Bson
open MongoDB.Bson.Serialization.Attributes

[<CLIMutable>]
type AddressDto = {
    [<BsonRepresentation(BsonType.ObjectId)>]
    Id: string
    FirstName: string
    LastName: string
    AddressLine1: string
    AddressLine2: string
    City: string
    PostalCode: string
    StateId: string
    StateName: string
    CountryCode: string
    IsBillingAddress: bool
}

[<CLIMutable>]
type StoreProfileDto = {
    [<BsonRepresentation(BsonType.ObjectId)>]
    Id: string
    StoreName: string
    ContactEmail: string
    SenderEmail: string
    BusinessName: string
    PhoneNumber: string
    TimeZoneId: string
    UnitSystem: string
    WeightUnit: string
    LengthUnit: string
    AddressId: string 
    [<BsonIgnore>]
    Address: AddressDto
}

[<CLIMutable>]
type CountryDto = {
    [<BsonRepresentation(BsonType.ObjectId)>]
    Id: string
    Name: string
    ISOCode: string
} 

[<CLIMutable>]
type StateDto = {
    [<BsonRepresentation(BsonType.ObjectId)>]
    Id: string
    CountryCode: string
    Name: string
}

[<CLIMutable>]
type ShippingOriginDto = {
    [<BsonRepresentation(BsonType.ObjectId)>]
    Id: string
    Name: string
    AddressId: string
    IsDefault: bool
}

[<CLIMutable>]
type ShippingCountryDto = {
    CountryCode: string
    StateIds: string seq
}

[<CLIMutable>]
type ShippingRateDto = {
    Min: decimal
    Max: decimal
    Rate: decimal
}

[<CLIMutable>]
type ShippingMethodDto = {
    Name: string
    MethodType: string
    Rates: ShippingRateDto seq
}

[<CLIMutable>] 
type ShippingZoneDto = {
    Name: string
    Methods: ShippingMethodDto seq
    Countries: ShippingCountryDto seq
}

[<CLIMutable>]
type ShippingProfileDto = {
    Id: string
    Name: string
    IsDefault: bool
    OriginIds: string seq
    Zones: ShippingZoneDto seq
}