namespace DuxCommerce.Core.Shared.InternalTypes

open DuxCommerce.Common
open DuxCommerce.Core.Shared.SimpleTypes

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