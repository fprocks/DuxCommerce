namespace DuxCommerce.Core.Shipping.Dto

open DuxCommerce.Common
open DuxCommerce.Core.Shipping.InternalTypes
open DuxCommerce.Core.Shipping.SimpleTypes
open DuxCommerce.Core.Shipping.PublicTypes
open DuxCommerce.Core.Shared.Dto
open DuxCommerce.Core.Shared.SimpleTypes


module StoreProfileDto =

    let toDomain dto : Result<StoreProfile, string> =
        result {
            let! storeName = String255.create "StoreName" dto.StoreName
            let! contactEmail = StoreContactEmail.create dto.ContactEmail
            let! senderEmail = StoreSenderEmail.create dto.SenderEmail
            let! businessName = String255.createOption "BusinessName" dto.BusinessName
            let! phoneNumber = String50.create "PhoneNumber" dto.PhoneNumber
            let! timeZoneId = TimeZoneId.create "TimeZoneId" dto.TimeZoneId
            let! unitSystem = UnitSystem.create dto.UnitSystem
            let! weightUnit = WeightUnit.create unitSystem dto.WeightUnit
            let! lengthUnit = LengthUnit.create unitSystem dto.LengthUnit
            let! address = AddressDto.toDomain dto.Address

            return
                { StoreProfileId = StoreProfileId.create dto.Id
                  StoreName = storeName
                  ContactEmail = contactEmail
                  SenderEmail = senderEmail
                  BusinessName = businessName
                  PhoneNumber = phoneNumber
                  TimeZoneId = timeZoneId
                  UnitSystem = unitSystem
                  WeightUnit = weightUnit
                  LengthUnit = lengthUnit
                  Address = address }
        }

module ShippingCountryDto =

    let toDomain (dto: ShippingCountryDto) =
        result {
            let! code = CountryCode.create "CountryCode" dto.CountryCode

            let! states =
                dto.StateNames
                |> Seq.map (String50.create "StateNames")
                |> Seq.toList
                |> Result.sequence

            return { CountryCode = code; States = states }
        }

module ShippingRateDto =

    let toDomain dto : ShippingRate =
        { Min = RateCondition.create dto.Min
          Max = RateCondition.create dto.Min
          Amount = RateAmount.create dto.Rate }

module ShippingMethodDto =

    let toDomain (dto: ShippingMethodDto) : Result<ShippingMethod, string> =
        result {
            let! name = String50.create "Method.Name" dto.Name
            let! methodType = ShippingMethodType.create dto.MethodType

            let rates =
                dto.Rates |> Seq.map ShippingRateDto.toDomain

            return
                { Name = name
                  MethodType = methodType
                  Rates = rates }
        }

module ShippingZoneDto =

    let toDomain (dto: ShippingZoneDto) : Result<ShippingZone, string> =
        result {
            let! name = String50.create "Zone.Name" dto.Name

            let! methods =
                dto.Methods
                |> Seq.map ShippingMethodDto.toDomain
                |> Seq.toList
                |> Result.sequence

            let! countries =
                dto.Countries
                |> Seq.map ShippingCountryDto.toDomain
                |> Seq.toList
                |> Result.sequence

            return
                { Name = name
                  Methods = methods
                  Countries = countries }
        }

module ShippingProfileDto =

    let toDomain (dto: ShippingProfileDto) : Result<ShippingProfile, string> =
        result {
            let! name = String50.create "Profile.Name" dto.Name

            let origins =
                dto.OriginIds |> Seq.map ShippingOriginId.create

            let! zones =
                dto.Zones
                |> Seq.map ShippingZoneDto.toDomain
                |> Seq.toList
                |> Result.sequence

            return
                { ShippingProfileId = ShippingProfileId.create ""
                  Name = name
                  Origins = origins
                  Zones = zones }
        }
