namespace DuxCommerce.Core.Shared.SimpleTypes

open DuxCommerce.Common

type StateId = private StateId of string

module StateId =

    let value (StateId id) = id
    let create id = StateId id


type CountryId = private CountryId of string

module CountryId =

    let value (CountryId id) = id
    let create id = CountryId id


type AddressId = private AddressId of string

module AddressId =

    let value (AddressId id) = id
    let create id = AddressId id

    let valueOption (value : AddressId option) = 
        match value with
        | Some (AddressId id) -> id
        | None -> null

    let createOption str = 
        ConstrainedType.createOption "AddressId" AddressId 12 str

type CountryCode = private CountryCode of String2

module CountryCode =
    let value (CountryCode code) = code
    let create field code =
        result {
            let! countryCode = String2.create field code
            return CountryCode countryCode
            }