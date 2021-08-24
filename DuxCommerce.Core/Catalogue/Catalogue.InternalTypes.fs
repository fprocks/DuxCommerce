namespace DuxCommerce.Core.Catalogue.InternalTypes

open DuxCommerce.Common
open DuxCommerce.Core.Catalogue.SimpleTypes

type ProductType =
    | PhysicalProduct
    | DigitalProduct

module ProductType =

    let value productType =
        match productType with
        | PhysicalProduct -> "PhysicalProduct"
        | DigitalProduct -> "DigitalProduct"

    let create productType =
        match productType with
        | "PhysicalProduct" -> Ok PhysicalProduct
        | "DigitalProduct" -> Ok DigitalProduct
        | _ ->
            let msg =
                "ProductType must be 'PhysicalProduct' or 'DigitalProduct'"

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
        | "Remove" -> Ok Remove
        | "ContinueSelling" -> Ok ContinueSelling
        | "StopSelling" -> Ok StopSelling
        | _ ->
            let msg =
                "OutOfStockRule must be one of 'Remove', 'ContinueSelling', 'StopSelling'"

            Error msg

type Product =
    { ProductId: ProductId
      Name: String255
      Description: string
      Price: SalePrice
      Retail: RetailPrice
      Cost: ProductCost
      Length: Length
      Width: Width
      Height: Height
      Weight: Weight
      ProductType: ProductType
      SKU: String255
      Barcode: String255
      TrackInventory: bool
      OutOfStockRule: OutOfStockRule }
