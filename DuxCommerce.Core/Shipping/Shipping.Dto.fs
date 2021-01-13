namespace DuxCommerce.Shipping.Dto

open DuxCommerce.Common
open DuxCommerce.Shipping.InternalTypes
open DuxCommerce.Shipping.SimpleTypes
open DuxCommerce.Shipping.PublicTypes

module AddressDto =
    let toDomain (dto: AddressDto) :Result<Address, string> =
        result {
            let addressId = AddressId.create dto.Id
            let! firstName = String50.create "FirstName" dto.FirstName
            let! lastName = String50.create "LastName" dto.LastName
            let! addressLine1 = String255.create "AddressLine1" dto.AddressLine1
            let! addressLine2 = String255.createOption "AddressLine2" dto.AddressLine2
            let! city = String100.create "City" dto.City
            let! postalCode = String50.createOption "PostalCode" dto.PostalCode
            let! state = String100.create "State" dto.StateName
            let! country = CountryCode.create "CountryCode" dto.CountryCode
            
            return {
                AddressId = addressId
                FirstName = firstName
                LastName = lastName
                AddressLine1 = addressLine1
                AddressLine2 = addressLine2
                City = city
                PostalCode = postalCode
                StateName = state
                CountryCode = country
            }
        }

module StoreProfileDto =
    let toDomain dto :Result<StoreProfile, string> =
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
            
            return {
                StoreProfileId = StoreProfileId.create dto.Id
                StoreName = storeName
                ContactEmail = contactEmail
                SenderEmail = senderEmail
                BusinessName = businessName
                PhoneNumber = phoneNumber
                TimeZoneId = timeZoneId
                UnitSystem = unitSystem
                WeightUnit = weightUnit
                LengthUnit = lengthUnit
                Address = address
              } 
        }

module ShippingCountryDto =
    let toDomain (dto:ShippingCountryDto) =
        result {
            let! code = CountryCode.create "CountryCode" dto.CountryCode
            let! states = dto.StateNames 
                        |> Seq.map (String50.create "StateNames")
                        |> Seq.toList
                        |> Result.sequence

            return {
                CountryCode = code
                States = states
            }
        }

module ShippingRateDto =
    let toDomain dto :ShippingRate=
        {
            Min = RateCondition.create dto.Min
            Max = RateCondition.create dto.Min
            Rate = RateAmount.create dto.Rate
        }
        
module ShippingMethodDto =
    let toDomain (dto:ShippingMethodDto) :Result<ShippingMethod, string> =
        result {
            let! name = String50.create "Method.Name" dto.Name
            let! methodType = ShippingMethodType.create dto.MethodType
            let rates = dto.Rates |> Seq.map ShippingRateDto.toDomain
                                    
            return {
                Name = name
                MethodType = methodType
                Rates = rates
            }
        }

module ShippingZoneDto = 
    let toDomain (dto:ShippingZoneDto) :Result<ShippingZone, string> =
        result {
            let! name = String50.create "Zone.Name" dto.Name
            let! methods = dto.Methods 
                            |> Seq.map ShippingMethodDto.toDomain
                            |> Seq.toList
                            |> Result.sequence
            let! countries = dto.Countries 
                            |> Seq.map ShippingCountryDto.toDomain
                            |> Seq.toList
                            |> Result.sequence
            return {
                Name = name
                Methods = methods
                Countries = countries
            }
        }

module ShippingProfileDto = 
    let toDomain (dto:ShippingProfileDto) :Result<ShippingProfile, string> =
        result {
            let! name = String50.create "Profile.Name" dto.Name
            let origins = dto.OriginIds |> Seq.map ShippingOriginId.create
            let! zones = dto.Zones 
                        |> Seq.map ShippingZoneDto.toDomain
                        |> Seq.toList
                        |> Result.sequence
            return {
                ShippingProfileId = ShippingProfileId.create ""
                Name = name
                Origins = origins
                Zones = zones
            }
        }        