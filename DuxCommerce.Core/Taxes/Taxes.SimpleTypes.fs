namespace DuxCommerce.Core.Taxes.SimpleTypes

type TaxRateId = private TaxRateId of string

module TaxRateId =
    let value (TaxRateId id) = id
    let create id = TaxRateId id


type TaxRateAmount = private TaxRateAmount of decimal

module TaxRateAmount =
    let value (TaxRateAmount amount) = amount
    let create amount = TaxRateAmount amount    