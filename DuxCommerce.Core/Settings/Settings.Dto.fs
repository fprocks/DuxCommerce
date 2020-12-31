namespace DuxCommerce.Settings.Dto

open DuxCommerce.Common
open DuxCommerce.Settings.InternalTypes
open DuxCommerce.Settings.SimpleTypes
  
[<CLIMutable>]
type CountryDto = {
    Id: int64
    Name: string
    ISOCode: string
} 

[<CLIMutable>]
type StateDto = {
    Id: int64
    CountryCode: string
    Name: string
}

[<CLIMutable>]
type AddressDto = {
    Id: int64
    FirstName: string
    LastName: string
    AddressLine1: string
    AddressLine2: string
    City: string
    PostalCode: string
    StateId: int64
    StateName: string
    CountryCode: string
    IsBillingAddress: bool
}

[<CLIMutable>]
type StoreProfileDto = {
    Id: int64
    StoreName: string
    ContactEmail: string
    SenderEmail: string
    BusinessName: string
    PhoneNumber: string
    TimeZoneId: string
    UnitSystem: string
    WeightUnit: string
    LengthUnit: string
    AddressId: int64
    Address: AddressDto
}

[<CLIMutable>]
type ShippingOriginDto = {
    Id: int64
    Name: string
    AddressId: int64
    IsDefault: bool
    Address: AddressDto
}

[<CLIMutable>]
type ShippingProfileOriginDto = {
    Id: int64
    ShippingProfileId: int64
    ShippingOriginId: int64
}

[<CLIMutable>]
type ShippingStateDto = {
    Id: int64
    ShippingCountryId: int64
    StateId: int64
}

[<CLIMutable>]
type ShippingCountryDto = {
    Id: int64
    ShippingZoneId: int64
    CountryCode: string
    States: StateDto seq
}

[<CLIMutable>]
type ShippingRateDto = {
    Id: int64
    ShippingZoneId: int64
    Name: string
    RateType: string
}

[<CLIMutable>]
type ShippingRateItemDto = {
    Id: int64
    ShippingRateId: int64
    Rate: decimal
    Min: decimal
    Max: decimal
}

[<CLIMutable>] 
type ShippingZoneDto = {
    Id: int64
    Name: string
    ShippingProfileId: int64
    Rates: ShippingRateDto seq
    Countries: ShippingCountryDto seq
}

[<CLIMutable>]
type ShippingProfileDto = {
    Id: int64
    Name: string
    IsDefault: bool
    Origins: ShippingOriginDto seq
    Zones: ShippingZoneDto seq
}

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