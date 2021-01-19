namespace DuxCommerce.Core.Catalogue.PublicTypes

open MongoDB.Bson.Serialization.Attributes
open MongoDB.Bson

[<CLIMutable>]
type ProductDto = {
    [<BsonId>]
    [<BsonRepresentation(BsonType.ObjectId)>]
    Id: string
    Name : string
    Description : string
    Price : decimal
    Retail : decimal
    Cost : decimal
    Length : decimal
    Width : decimal
    Height : decimal
    Weight: decimal
    ProductType : string
    SKU : string
    Barcode : string
    TrackInventory : bool
    OutOfStockRule : string
}