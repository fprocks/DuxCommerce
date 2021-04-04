namespace DuxCommerce.Core.Shipping.PublicTypes

open MongoDB.Bson
open MongoDB.Bson.Serialization.Attributes
open DuxCommerce.Core.Shared.PublicTypes


[<CLIMutable>]
type StoreProfileDto = {
    [<BsonId>]
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
type ShippingOriginDto = {
    [<BsonId>]
    [<BsonRepresentation(BsonType.ObjectId)>]
    Id: string
    Name: string
    Address: AddressDto
    IsDefault: bool
    }

[<CLIMutable>]
type ShippingCountryDto = {
    CountryCode: string
    // Todo: change this to States: StateDto seq
    StateNames: string seq
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
    [<BsonId>]
    [<BsonRepresentation(BsonType.ObjectId)>]
    Id: string
    Name: string
    IsDefault: bool
    OriginIds: string seq
    Zones: ShippingZoneDto seq
    }