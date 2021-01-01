namespace DuxCommerce.Settings.PublicTypes

[<CLIMutable>]
type AddressDto = {
    Id: int64
    FirstName: string
    LastName: string
    AddressLine1: string
    AddressLine2: string
    City: string
    PostalCode: string
    StateId: int64
    StateName: string
    CountryCode: string
    IsBillingAddress: bool
}

[<CLIMutable>]
type StoreProfileDto = {
    Id: int64
    StoreName: string
    ContactEmail: string
    SenderEmail: string
    BusinessName: string
    PhoneNumber: string
    TimeZoneId: string
    UnitSystem: string
    WeightUnit: string
    LengthUnit: string
    AddressId: int64
    Address: AddressDto
}

[<CLIMutable>]
type CountryDto = {
  Id: int64
  Name: string
  ISOCode: string
} 

[<CLIMutable>]
type StateDto = {
  Id: int64
  CountryCode: string
  Name: string
}

[<CLIMutable>]
type ShippingOriginDto = {
    Id: int64
    Name: string
    AddressId: int64
    IsDefault: bool
    Address: AddressDto
}

[<CLIMutable>]
type ShippingProfileOriginDto = {
    Id: int64
    ShippingProfileId: int64
    ShippingOriginId: int64
}

[<CLIMutable>]
type ShippingStateDto = {
    Id: int64
    ShippingCountryId: int64
    StateId: int64
}

[<CLIMutable>]
type ShippingCountryDto = {
    Id: int64
    ShippingZoneId: int64
    CountryCode: string
    StateIds: int64 seq
}

[<CLIMutable>]
type ShippingRateDto = {
    Id: int64
    ShippingMethodId: int64
    Min: decimal
    Max: decimal
    Rate: decimal
}

[<CLIMutable>]
type ShippingMethodDto = {
    Id: int64
    ShippingZoneId: int64
    Name: string
    MethodType: string
    Rates: ShippingRateDto seq
}

[<CLIMutable>] 
type ShippingZoneDto = {
    Id: int64
    Name: string
    ShippingProfileId: int64
    Methods: ShippingMethodDto seq
    Countries: ShippingCountryDto seq
}

[<CLIMutable>]
type ShippingProfileDto = {
    Id: int64
    Name: string
    IsDefault: bool
    OriginIds: int64 seq
    Zones: ShippingZoneDto seq
}