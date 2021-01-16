namespace DuxCommerce.Core.Shared.Dto

open DuxCommerce.Core.Shared.PublicTypes
open DuxCommerce.Core.Shared.InternalTypes
open DuxCommerce.Common
open DuxCommerce.Core.Shared.SimpleTypes

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