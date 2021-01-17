namespace DuxCommerce.Core.Shared.SimpleTypes

open DuxCommerce.Common

type AddressId = private AddressId of string
module AddressId =
    let value (AddressId id) = id
    let create id = AddressId id

    
type CountryCode = private CountryCode of String2
module CountryCode =
    let value (CountryCode code) = code
    let create field code =
        result {
            let! countryCode = String2.create field code
            return CountryCode countryCode
        }