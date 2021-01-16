namespace DuxCommerce.Core.Shipping.InternalTypes

open DuxCommerce.Common
open DuxCommerce.Core.Shipping.SimpleTypes

type ShippingMethodType = 
    | ByWeight
    | ByQuantity
    | ByOrderTotal

module ShippingMethodType =

    let value methodType =
        match methodType with
        | ByWeight -> "ByWeight"
        | ByQuantity -> "ByQuantity"
        | ByOrderTotal -> "ByOrderTotal"

    let create methodType =
        match methodType with
        | "ByWeight"  -> 
            Ok ByWeight
        | "ByQuantity" -> 
            Ok ByQuantity
        | "ByOrderTotal" -> 
            Ok ByOrderTotal
        | _ -> 
            let msg = "ShippingRateType must be one of 'ByWeight', 'ByQuantity', 'ByOrderTotal'" 
            Error msg       

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
 
type ShippingCountry = {
    CountryCode: CountryCode
    States: String50 seq
}

type ShippingRate = {
    Min: RateCondition
    Max: RateCondition
    Rate: RateAmount
}

type ShippingMethod = {
    Name: String50
    MethodType: ShippingMethodType
    Rates: ShippingRate seq
}

type ShippingZone = {
    Name: String50
    Methods: ShippingMethod seq
    Countries: ShippingCountry seq
}
type ShippingProfile = {
    ShippingProfileId: ShippingProfileId
    Name: String50
    Origins: ShippingOriginId seq
    Zones: ShippingZone seq
}