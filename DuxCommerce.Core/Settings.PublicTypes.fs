namespace DuxCommerce.Settings.PublicTypes

type AddressDto = {
    Id : int64
    Address1 : string
    Address2 : string
    Address3 : string
    City : string
    PostalCode : string
    State : string
    Country : string
}

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
    Address : AddressDto
}
