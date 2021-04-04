namespace DuxCommerce.Core.Shipping.InternalTypes

open DuxCommerce.Common
open DuxCommerce.Core.Shipping.SimpleTypes
open DuxCommerce.Core.Shared.InternalTypes
open DuxCommerce.Core.Shared.SimpleTypes

type ImperialWeightUnit =
    | Pound
    | Ounce
       
type ImperialLengthUnit =
    | Inch
    | Foot

type ImperialSystem = {
    WeightUnit : ImperialWeightUnit
    LengthUnit : ImperialLengthUnit
}

type MetricWeightUnit =
    | Kilogram
    | Gram
    
type MetricLengthUnit =
    | Meter
    | Centimeter
    
type MetricSystem = {
    WeightUnit : MetricWeightUnit
    LengthUnit : MetricLengthUnit
}    

type UnitSystem =
    | ImperialSystem
    | MetricSystem
        
module UnitSystem =
    let value system =
        match system with
        | ImperialSystem _ -> "ImperialSystem"
        | MetricSystem _ -> "MetricSystem"
                 
    let create system =
        match system with
        | "ImperialSystem" -> Ok ImperialSystem
        | "MetricSystem" -> Ok MetricSystem
        | _ ->
            let msg = "UnitSystem must be 'ImperialSystem' or 'MetricSystem'" 
            Error msg
            
type WeightUnit =
    | Imperial of ImperialWeightUnit
    | Metric of MetricWeightUnit
    
module WeightUnit =
    let value weight =
        match weight with
        | Imperial Pound -> "Pound"
        | Imperial Ounce -> "Ounce"
        | Metric Kilogram -> "Kilogram"
        | Metric Gram -> "Gram"
        
    let create (system:UnitSystem) weight =
        match system, weight with
        | ImperialSystem, "Pound" -> Ok (Imperial Pound)
        | ImperialSystem, "Ounce" -> Ok (Imperial Ounce)
        | ImperialSystem, _ -> Error "ImperialWeightUnit must be 'Pound' or 'Ounce'" 
        | MetricSystem, "Kilogram" -> Ok (Metric Kilogram)
        | MetricSystem, "Gram" -> Ok (Metric Gram)
        | MetricSystem, _ -> Error "MetricWeightUnit must be 'Kilogram' or 'Gram'" 
        
type LengthUnit =
    | Imperial of ImperialLengthUnit
    | Metric of MetricLengthUnit
    
module LengthUnit =
    let value length =
        match length with
        | Imperial Inch -> "Inch"
        | Imperial Foot -> "Foot"
        | Metric Centimeter -> "Centimeter"
        | Metric Meter -> "Meter"
        
    let create system lengthUnit =
        match system, lengthUnit with
        | ImperialSystem, "Inch" -> Ok (Imperial Inch)
        | ImperialSystem, "Foot" -> Ok (Imperial Foot)
        | ImperialSystem, _ -> Error "ImperialLengthUnit must be 'Inch' or 'Foot'" 
        | MetricSystem, "Centimeter" -> Ok (Metric Centimeter)
        | MetricSystem, "Meter" -> Ok (Metric Meter)
        | MetricSystem, _ -> Error "MetricLengthUnit must be 'Centimeter' or 'Meter'" 

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
    Amount: ShippingRateAmount
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