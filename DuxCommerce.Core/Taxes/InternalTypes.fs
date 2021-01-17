namespace DuxCommerce.Core.Taxes.InternalTypes

open DuxCommerce.Core.Shared.SimpleTypes
open DuxCommerce.Core.Shared.InternalTypes
open DuxCommerce.Common
open DuxCommerce.Core.Taxes.SimpleTypes

type TaxState = {
    State: State
    PostalCodes: String50 list
}

type TaxCountry = {
    CountryCode: CountryCode
    States: TaxState list
}

type TaxZone = {
    Name: String50
    Countries: TaxCountry list
}

type TaxRate = {
    TaxRateId: TaxRateId
    Name: String50
    Amount:TaxRateAmount
    Zone: TaxZone
}