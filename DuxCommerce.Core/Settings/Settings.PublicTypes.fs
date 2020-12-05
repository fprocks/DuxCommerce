namespace DuxCommerce.Settings.PublicTypes

[<CLIMutable>]
type AddressDto = {
    Id : int64
    FirstName : string
    LastName : string
    AddressLine1 : string
    AddressLine2 : string
    City : string
    PostalCode : string
    State : string
    Country : string
}

[<CLIMutable>]
type StoreDetailsDto = {
    Id : int64
    StoreName : string
    ContactEmail : string
    SenderEmail : string
    BusinessName : string
    PhoneNumber: string
    TimeZoneId : string
    UnitSystem : string
    WeightUnit : string
    LengthUnit : string
    AddressId: int64
    Address : AddressDto
}

[<CLIMutable>]
type LocationDto = {
    Id : int64
    Name : string
    AddressId : int64
    IsDefault : bool
}

[<CLIMutable>]
type ShippingProfileDto = {
    Id: int64
    Name : string
}

type ShippingLocationDto = {
    Id : int64
    ShippingProfileId : int64
    LocationId : int64
}

type ShippingZoneDto = {
    Id : int64
    Name : string
    ShippingProfileId : int64
}

type ShippingCountryDto = {
    Id : int64
    ShippingZoneId : int64
    CountryId : int64
}

type ShippingStateDto = {
    Id : int64
    ShippingCountryId : int64
    StateId : int64
}

type ShippingRatesDto = {
    Id : int64
    ShippingZoneId : int64
    Name : string
    ShippingType : string
    Rate : decimal
    Min : decimal
    Max : decimal
}

