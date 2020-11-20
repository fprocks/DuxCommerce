namespace DuxCommerce.Settings.InternalTypes

open DuxCommerce.Common
open DuxCommerce.Settings.SimpleTypes

type Address = {
    AddressId : AddressId
    Address1 : String255
    Address2 : String255 option
    Address3 : String255 option
    City : String100
    PostalCode : String50 option
    State : String100 option
    Country : CountryCode
}

type StoreDetails = {
    StoreId : StoreId
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
 
  