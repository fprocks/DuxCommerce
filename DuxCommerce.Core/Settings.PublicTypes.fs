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
type WarehouseDto = {
    Id : int64
    StoreId : int64
    Name : string
    AddressId : int64
    IsDefault : bool
}