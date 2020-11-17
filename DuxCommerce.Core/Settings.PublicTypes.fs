namespace DuxCommerce.Settings

type StoreDetailsDto = {
    Id : int64
    StoreName : string
    ContactEmail : string
    SenderEmail : string
    BusinessName : string
    TimeZoneId : string
    UnitSystem : string
    WeightUnit : string
    LengthUnit : string
    AddressId : int64 option
}

type AddressDto = {
    Id : int64
    StreetAddress1 : string
    StreetAddress2 : string option
    StreetAddress3 : string option
    City : string
    PostalCode : string option
    StateId : int64 option
    StateName : string option
    CountryId : int64
    CountryName : string
}