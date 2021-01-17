namespace DuxCommerce.Core.Taxes.PublicTypes

open MongoDB.Bson.Serialization.Attributes
open MongoDB.Bson
open DuxCommerce.Core.Shared.PublicTypes

[<CLIMutable>]
type TaxStateDto = {
    State: StateDto
    PostalCodes: string seq
}

[<CLIMutable>]
type TaxCountryDto = {
    CountryCode: string
    States: TaxStateDto seq
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
    Rate: decimal
    Zone: TaxZoneDto 
}