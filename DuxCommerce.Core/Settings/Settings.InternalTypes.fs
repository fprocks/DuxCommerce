namespace DuxCommerce.Settings.InternalTypes

open DuxCommerce.Common
open DuxCommerce.Settings.SimpleTypes
open DuxCommerce.Settings.PublicTypes

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
 
type ShippingCountry = {
    CountryCode: CountryCode
    States: StateId seq
}

module ShippingCountry =
    let fromRequest (request:ShippingCountryRequest) =
        result {
            let! code = CountryCode.create "Country.Code" request.CountryCode
            let states = request.States |> Seq.map StateId.create

            return {
                CountryCode = code
                States = states
            }
        }

type ShippingRate = {
    Min: RateCondition
    Max: RateCondition
    Rate: RateAmount
}

module ShippingRate =
    let fromRequest (request:ShippingRateRequest) =
        {
            Min = RateCondition.create request.Min
            Max = RateCondition.create request.Min
            Rate = RateAmount.create request.Rate
        }

type ShippingMethod = {
    Name: String50
    MethodType: ShippingMethodType
    Rates: ShippingRate seq
}

module ShippingMethod =
    let fromRequest (request:ShippingMethodRequest)=
        result {
            let! name = String50.create "Method.Name" request.Name
            let! methodType = ShippingMethodType.create request.MethodType
            let rates = request.Rates |> Seq.map ShippingRate.fromRequest
                            
            return {
                Name = name
                MethodType = methodType
                Rates = rates
            }
        }

type ShippingZone = {
    Name: String50
    Methods: ShippingMethod seq
    Countries: ShippingCountry seq
}

module ShippingZone = 
    let fromRequest (request:ShippingZoneRequest) =
        result {
            let! name = String50.create "Zone.Name" request.Name
            let! methods = request.Methods 
                            |> Seq.map ShippingMethod.fromRequest
                            |> Seq.toList
                            |> Result.sequence
            let! countries = request.Countries 
                            |> Seq.map ShippingCountry.fromRequest
                            |> Seq.toList
                            |> Result.sequence
            return {
                Name = name
                Methods = methods
                Countries = countries
            }
        }


type ShippingProfile = {
    ShippingProfileId: ShippingProfileId
    Name: String50
    Origins: ShippingOriginId seq
    Zones: ShippingZone seq
}

module ShippingProfile = 
    let fromRequest (request:ShippingProfileRequest) :Result<ShippingProfile, string> =
        result {
            let! name = String50.create "Profile.Name" request.Name
            let origins = request.Origins |> Seq.map ShippingOriginId.create
            let! zones = request.Zones 
                        |> Seq.map ShippingZone.fromRequest
                        |> Seq.toList
                        |> Result.sequence
            return {
                ShippingProfileId = ShippingProfileId.create 0L
                Name = name
                Origins = origins
                Zones = zones
            }
        }  