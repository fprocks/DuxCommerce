namespace DuxCommerce.Core.Shared.Dto

open DuxCommerce.Core.Shared.PublicTypes
open DuxCommerce.Core.Shared.InternalTypes
open DuxCommerce.Common
open DuxCommerce.Core.Shared.SimpleTypes

module StateDto =

    let toDomain (dto: StateDto) :Result<State, string> = 
        result {
            let! countryCode = CountryCode.create "CountryCode" dto.CountryCode
            let! name = String50.create "StateName" dto.Name
            return {
                StateId = StateId.create ""
                CountryCode = countryCode
                Name = name
            }
        }

module AddressDto =

    let toDomain (dto: AddressDto) :Result<Address, string> =
        result {
            let! addressId = AddressId.createOption dto.Id
            let! firstName = String50.create "FirstName" dto.FirstName
            let! lastName = String50.create "LastName" dto.LastName
            let! addressLine1 = String255.create "AddressLine1" dto.AddressLine1
            let! addressLine2 = String255.createOption "AddressLine2" dto.AddressLine2
            let! city = String100.create "City" dto.City
            let! postalCode = String50.createOption "PostalCode" dto.PostalCode
            let stateId = StateId.create dto.StateId
            let! stateName = String100.create "State" dto.StateName
            let! country = CountryCode.create "CountryCode" dto.CountryCode
            
            return {
                AddressId = addressId
                FirstName = firstName
                LastName = lastName
                AddressLine1 = addressLine1
                AddressLine2 = addressLine2
                City = city
                PostalCode = postalCode
                StateId = stateId
                StateName = stateName
                CountryCode = country
            }
        }

    let fromDomain (address:Address) :AddressDto = 
        {
            Id = AddressId.valueOption address.AddressId
            FirstName = String50.value address.FirstName
            LastName = String50.value address.LastName
            AddressLine1 = String255.value address.AddressLine1
            AddressLine2 = String255.valueOption address.AddressLine2
            City = String100.value address.City
            PostalCode = String50.valueOption address.PostalCode
            StateId = StateId.value address.StateId
            StateName = String100.value address.StateName
            CountryCode = 
                address.CountryCode 
                |> CountryCode.value 
                |> String2.value
        }