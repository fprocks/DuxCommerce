namespace DuxCommerce.Settings.InternalTypes

open DuxCommerce.Common
open DuxCommerce.Settings.SimpleTypes

type StoreDetails = {
    Id : StoreId
    StoreName : String255
    ContactEmail : StoreDetails
    SenderEmail : StoreSenderEmail
    BusinessName : String255 option
    PhoneNumber: String50
    TimeZoneId : TimeZoneId
    UnitSystem : UnitSystem
    WeightUnit : WeightUnit
    LengthUnit : LengthUnit
    AddressId : AddressId option
}

type Address = {
    Id : AddressId
    Address1 : String255
    Address2 : String255 option
    City : String100
    PostalCode : String50
    State : String100 option
    Country : CountryCode
}
 
  