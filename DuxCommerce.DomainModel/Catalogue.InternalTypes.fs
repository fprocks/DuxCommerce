namespace DuxCommerce.DomainModel.Catalogue

open DuxCommerce.DomainModel.Common

type ProductShippable =
    | PhysicalProduct
    | DigitalProduct
    | ShipSeparately
    
 type OutOfStockMode =
     | Remove
     | ContinueSelling
     | StopSelling
     
 type ProductPriceType =
     | Price of SalePrice
     | Retail of RetailPrice
     | Cost of ProductCost
     
type Product = {
    Id : ProductId
    Name : String255
    Description : string
    Price : SalePrice
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