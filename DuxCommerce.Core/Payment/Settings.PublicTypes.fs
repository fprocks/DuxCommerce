namespace DuxCommerce.Payment.PublicTypes

open MongoDB.Bson
open MongoDB.Bson.Serialization.Attributes

[<CLIMutable>]
type PaymentMethodDto = {
    [<BsonId>]
    [<BsonRepresentation(BsonType.ObjectId)>]
    Id: string
    Name: string
    Type: string
    AdditionalDetails: string
    PaymentInstructions: string
}