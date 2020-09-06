namespace DuxCommerce.DomainModel.Catalogue

open System.Net.Security
open DuxCommerce.DomainModel.Common

type ProductShippable =
    | PhysicalProduct
    | DigitalProduct
    | ShipSeparately
 type OutOfStockMode =
     | Remove
     | ContinueSelling
     | StopSelling
type Product = {
    Id : ProductId
    Name : String255
    Description : string
    Price : ProductPrice
    Retail : RetailPrice
    Cost : ProductCost
    Length : Length
    Width : Width
    Height : Height
    Weight: Weight
    Shippable : ProductShippable
    SKU : String100
    Barcode : String100
    TrackInventory : bool
    OutOfStockMode : OutOfStockMode
}
    