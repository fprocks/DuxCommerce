namespace DuxCommerce.Catalogue

open DuxCommerce.Common

type CreateProductRequest = {
    Name : string
    Description : string
    Price : decimal
    Retail : decimal
    Cost : decimal
    Length : decimal
    Width : decimal
    Height : decimal
    Weight: decimal
    Shippable : string
    SKU : string
    Barcode : string
    TrackInventory : bool
    OutOfStockHandling : string
}

module CreateProductRequest =
    let toDomain (request: CreateProductRequest) : Result<Product, string> =
        result {
            let id = ProductId (int64 0)
            let! name = String256.create "Name" request.Name
            let price = SalePrice request.Price
            let retail = RetailPrice request.Retail
            let cost = ProductCost request.Cost
            let length = Length request.Length
            let width = Width request.Width
            let height = Height request.Height
            let weight = Weight request.Weight
            let! shippingType = ShippingType.create "ShippingType" request.Shippable
            let! sku = String100.create "SKU" request.SKU
            let! barcode = String50.create "Barcode" request.Barcode
            let! outOfStockHandling = OutOfStockHandling.create "OutOfStockHandling" request.OutOfStockHandling
            return {
                Id = id
                Name = name
                Description = request.Description
                Price = price
                Retail = retail
                Cost = cost
                Length = length
                Width = width
                Height = height
                Weight = weight
                ShippingType = shippingType
                SKU = sku
                Barcode = barcode
                TrackInventory = request.TrackInventory
                OutOfStockHandling = outOfStockHandling
                }
            }

type ValidationError = ValidationError of string
type DatabaseError = DatabaseError of string

type CreateProductError =
    | ValidationError of ValidationError
    | DatabaseError of DatabaseError
    