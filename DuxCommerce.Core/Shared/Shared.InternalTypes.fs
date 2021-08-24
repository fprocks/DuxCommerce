namespace DuxCommerce.Core.Shared.InternalTypes

open DuxCommerce.Common
open DuxCommerce.Core.Shared.SimpleTypes

type Address =
    { AddressId: AddressId option
      FirstName: String50
      LastName: String50
      AddressLine1: String255
      AddressLine2: String255 option
      City: String100
      PostalCode: String50 option
      StateId: StateId
      StateName: String100
      CountryCode: CountryCode }

type State =
    { StateId: StateId
      CountryCode: CountryCode
      Name: String50 }

type Country =
    { CountryId: CountryId
      Name: String50
      ISOCode: CountryCode }
