namespace DuxCommerce.Settings.InternalTypes

open DuxCommerce.Common
open DuxCommerce.Settings.SimpleTypes

type Address = {
    AddressId : AddressId
    FirstName : String50
    LastName : String50
    AddressLine1 : String255
    AddressLine2 : String255 option
    City : String100
    PostalCode : String50 option
    StateName : String100
    CountryCode : CountryCode
}

type StoreProfile = {
    StoreProfileId : StoreProfileId
    StoreName : String255
    ContactEmail : StoreContactEmail
    SenderEmail : StoreSenderEmail
    BusinessName : String255 option
    PhoneNumber: String50
    TimeZoneId : TimeZoneId
    UnitSystem : UnitSystem
    WeightUnit : WeightUnit
    LengthUnit : LengthUnit
    Address : Address
}
 
  