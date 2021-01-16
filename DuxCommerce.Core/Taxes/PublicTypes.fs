namespace DuxCommerce.Core.Taxes.PublicTypes

open MongoDB.Bson.Serialization.Attributes
open MongoDB.Bson

[<CLIMutable>]
type TaxRateDto = {
    [<BsonId>]
    [<BsonRepresentation(BsonType.ObjectId)>]
    Id: string
    Name: string
    CountryCode: string
    StateId: string
    StateName: string
    Rate: decimal
}