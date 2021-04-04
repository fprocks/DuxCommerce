namespace DuxCommerce.Core.Shared.PublicTypes

open MongoDB.Bson.Serialization.Attributes
open MongoDB.Bson

[<CLIMutable>]
type AddressDto = {
    [<BsonId>]
    [<BsonRepresentation(BsonType.ObjectId)>]
    Id: string
    FirstName: string
    LastName: string
    AddressLine1: string
    AddressLine2: string
    City: string
    PostalCode: string
    StateId: string
    StateName: string
    CountryCode: string
    }

[<CLIMutable>]
type CountryDto = {
    [<BsonId>]
    [<BsonRepresentation(BsonType.ObjectId)>]
    Id: string
    Name: string
    ISOCode: string
    } 

[<CLIMutable>]
type StateDto = {
    [<BsonId>]
    [<BsonRepresentation(BsonType.ObjectId)>]
    Id: string
    CountryCode: string
    Name: string
    }
