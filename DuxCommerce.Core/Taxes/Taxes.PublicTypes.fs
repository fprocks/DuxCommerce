namespace DuxCommerce.Core.Taxes.PublicTypes

open MongoDB.Bson.Serialization.Attributes
open MongoDB.Bson
open DuxCommerce.Core.Shared.PublicTypes

[<CLIMutable>]
type TaxCountryDto = {
    CountryCode: string
    States: StateDto seq
    PostalCodes: string seq
    }

[<CLIMutable>] 
type TaxZoneDto = {
    Name: string
    Countries: TaxCountryDto seq
    }

[<CLIMutable>]
type TaxRateDto = {
    [<BsonId>]
    [<BsonRepresentation(BsonType.ObjectId)>]
    Id: string
    Name: string
    Amount: decimal
    Zone: TaxZoneDto 
    }