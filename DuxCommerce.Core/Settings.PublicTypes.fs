namespace DuxCommerce.Settings.PublicTypes

type StoreDetailsDto = {
    Id : int64
    StoreName : string
    ContactEmail : string
    SenderEmail : string
    BusinessName : string option
    PhoneNumber: string
    TimeZoneId : string
    UnitSystem : string
    WeightUnit : string
    LengthUnit : string
    AddressId : int64 option
}

type AddressDto = {
    Id : int64
    Address1 : string
    Address2 : string option
    Address3 : string option
    City : string
    PostalCode : string option
    State : string option
    Country : string
}