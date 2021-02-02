namespace DuxCommerce.Core.Checkout.PublicTypes

open MongoDB.Bson.Serialization.Attributes
open MongoDB.Bson


[<CLIMutable>]
type CustomerDto = {
    [<BsonId>]
    [<BsonRepresentation(BsonType.ObjectId)>]
    Id: string
    Username: string
    Password: string
    Email: string
    PhoneNumber: string
    FirstName: string
    LastName: string
    UserId: string
}