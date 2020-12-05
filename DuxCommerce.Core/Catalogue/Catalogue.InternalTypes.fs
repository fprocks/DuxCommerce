namespace DuxCommerce.Catalogue.InternalTypes

open DuxCommerce.Common
open DuxCommerce.Catalogue.SimpleTypes

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

    let create shippingType =
        match shippingType with
        | "PhysicalProduct"  -> 
            Ok PhysicalProduct
        | "DigitalProduct" -> 
            Ok DigitalProduct
        | "ShipSeparately" -> 
            Ok ShipSeparately
        | _ -> 
            let msg = "ShippingType must be one of 'PhysicalProduct', 'DigitalProduct', 'ShipSeparately'" 
            Error msg
            
type OutOfStockRule =
     | Remove
     | ContinueSelling
     | StopSelling
     
module OutOfStockRule =

    let value outofstockRule = 
        match outofstockRule with
        | Remove -> "Remove"
        | ContinueSelling -> "ContinueSelling"
        | StopSelling -> "StopSelling"

    let create outofstockRule =
        match outofstockRule with
        | "Remove"  -> 
            Ok Remove
        | "ContinueSelling" -> 
            Ok ContinueSelling
        | "StopSelling" -> 
            Ok StopSelling
        | _ -> 
            let msg = "OutOfStockRule must be one of 'Remove', 'ContinueSelling', 'StopSelling'" 
            Error msg        
     
type Product = {
    ProductId : ProductId
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