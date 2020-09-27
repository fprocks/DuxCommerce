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
        | "PhysicalProduct"  -> 
            Ok PhysicalProduct
        | "DigitalProduct" -> 
            Ok DigitalProduct
        | "ShipSeparately" -> 
            Ok ShipSeparately
        | _ -> 
            let msg = sprintf "%s: Must be one of 'PhysicalProduct', 'DigitalProduct', 'ShipSeparately'" fieldName 
            Error msg
            
type OutOfStockRule =
     | Remove
     | ContinueSelling
     | StopSelling
     
module OutOfStockRule =
    let value stockRule = 
        match stockRule with
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
    Name : String255
    Description : string
    Price : SalePrice
    Retail : RetailPrice
    Cost : ProductCost
    Length : Length
    Width : Width
    Height : Height
    Weight: Weight
    ShippingType : ShippingType
    SKU : String255
    Barcode : String255
    TrackInventory : bool
    OutOfStockRule : OutOfStockRule
}