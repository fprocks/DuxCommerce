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

type ShippingProfileSouce = {
    Id : int64
    ProfileId : int64
    LocationId : int64
}

type ShippingProfileZone = {
    Id : int64
    Name : string
    ProfileId : int64
}

type ShippingCountry = {
    Id : int64

}