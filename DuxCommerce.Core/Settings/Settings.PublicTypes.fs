namespace DuxCommerce.Settings.PublicTypes

[<CLIMutable>]
type CountryDto = {
    Id: int64
    CountryName: string
    ISOCode: string
    Enabled: bool
}

[<CLIMutable>]
type AddressDto = {
    Id: int64
    FirstName: string
    LastName: string
    AddressLine1: string
    AddressLine2: string
    City: string
    PostalCode: string
    State: string
    Country: string
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
type ShippingOriginDto = {
    Id: int64
    Name: string
    AddressId: int64
    IsDefault: bool
}

[<CLIMutable>]
type ShippingProfileOriginDto = {
    Id: int64
    ProfileId: int64
    OriginId: int64
}

[<CLIMutable>]
type ShippingStateDto = {
    Id: int64
    ShippingCountryId: int64
    State: string
}

[<CLIMutable>]
type ShippingCountryDto = {
    Id: int64
    ShippingZoneId: int64
    Country: string
    States: ShippingStateDto seq
}

[<CLIMutable>]
type ShippingRateDto = {
    Id: int64
    ShippingZoneId: int64
    Name: string
    RateType: string
    Rate: decimal
    Min: decimal
    Max: decimal
}

[<CLIMutable>]
type ShippingRateItemDto = {
    Id: int64
    ShippingRateId: int64
    Rate: decimal
    Min: decimal
    Max: decimal
}

[<CLIMutable>] 
type ShippingZoneDto = {
    Id: int64
    Name: string
    ShippingProfileId: int64
    Rates: ShippingRateDto seq
    Countries: ShippingCountryDto seq
}

[<CLIMutable>]
type ShippingProfileDto = {
    Id: int64
    Name: string
    IsDefault: bool
    Origins: ShippingProfileOriginDto seq
    Zones: ShippingZoneDto seq
}