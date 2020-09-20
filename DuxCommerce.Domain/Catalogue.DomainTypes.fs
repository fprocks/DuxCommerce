namespace DuxCommerce.Catalogue

open DuxCommerce.Common

type ShippingType =
    | PhysicalProduct
    | DigitalProduct
    | ShipSeparately
    
module ShippingType =
    let value shippingType = 
        match shippingType with
        | PhysicalProduct -> "PhysicalProduct"
        | DigitalProduct -> "DigitalProduct"
        | ShipSeparately -> "ShipSeparately"

    let create fieldName shippingType =
        match shippingType with
        | "physicalproduct"  -> 
            Ok PhysicalProduct
        | "digitalproduct" -> 
            Ok DigitalProduct
        | "shipseparately" -> 
            Ok ShipSeparately
        | _ -> 
            let msg = sprintf "%s: Must be one of 'PhysicalProduct', 'DigitalProduct', 'ShipSeparately'" fieldName 
            Error msg
            
 type OutOfStockHandling =
     | Remove
     | ContinueSelling
     | StopSelling
     
module OutOfStockHandling =
    let value stockHandling = 
        match stockHandling with
        | Remove -> "Remove"
        | ContinueSelling -> "ContinueSelling"
        | StopSelling -> "StopSelling"

    let create fieldName stockHandling =
        match stockHandling with
        | "Remove"  -> 
            Ok Remove
        | "ContinueSelling" -> 
            Ok ContinueSelling
        | "StopSelling" -> 
            Ok StopSelling
        | _ -> 
            let msg = sprintf "%s: Must be one of 'Remove', 'ContinueSelling', 'StopSelling'" fieldName 
            Error msg        
     
type Product = {
    Id : ProductId
    Name : String256
    Description : string
    Price : SalePrice
    Retail : RetailPrice
    Cost : ProductCost
    Length : Length
    Width : Width
    Height : Height
    Weight: Weight
    ShippingType : ShippingType
    SKU : String100
    Barcode : String50
    TrackInventory : bool
    OutOfStockHandling : OutOfStockHandling
}