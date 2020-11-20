namespace DuxCommerce.Settings.Dto

open DuxCommerce.Common
open DuxCommerce.Settings.InternalTypes
open DuxCommerce.Settings.PublicTypes
open DuxCommerce.Settings.SimpleTypes
     
module AddressDto =
    let toDomain (dto: AddressDto) :Result<Address, string> =
        result {
            let addressId = AddressId.create dto.Id
            let! address1 = String255.create "Address1" dto.Address1
            let! address2 = String255.createOption "Address2" dto.Address2
            let! address3 = String255.createOption "Address3" dto.Address3            
            let! city = String100.create "City" dto.City
            let! postalCode = String50.createOption "PostalCode" dto.PostalCode
            let! state = String100.createOption "State" dto.State
            let! country = CountryCode.create "CountryCode" dto.Country
            
            return {
                AddressId = addressId
                Address1 = address1
                Address2 = address2
                Address3 = address3
                City = city
                PostalCode = postalCode
                State = state
                Country = country
            }
        }
        
module StoreDetailsDto =
    let toDomain (dto: StoreDetailsDto) :Result<StoreDetails, string> =
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
                StoreId = StoreId.create dto.Id
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