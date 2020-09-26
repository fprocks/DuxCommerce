namespace DuxCommerce.Catalogue

open DuxCommerce.Common

type Product = {
    Id : int64
    Name : string
    Description : string
    Price : decimal
    Retail : decimal
    Cost : decimal
    Length : decimal
    Width : decimal
    Height : decimal
    Weight: decimal
    ShippingType : string
    SKU : string
    Barcode : string
    TrackInventory : bool
    OutOfStockRule : string
}

module CreateProductRequest =
    let toDomain (request: Product) : Result<ProductModel, string> =
        result {
            let id = ProductId (int64 0)
            let! name = String255.create "Name" request.Name
            let price = SalePrice request.Price
            let retail = RetailPrice request.Retail
            let cost = ProductCost request.Cost
            let length = Length request.Length
            let width = Width request.Width
            let height = Height request.Height
            let weight = Weight request.Weight
            let! shippingType = ShippingType.create "ShippingType" request.ShippingType
            let! sku = String255.create "SKU" request.SKU
            let! barcode = String255.create "Barcode" request.Barcode
            let! outOfStockRule = OutOfStockRule.create "OutOfStockRule" request.OutOfStockRule
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
                OutOfStockRule = outOfStockRule
                }
            }

type ValidationError = ValidationError of string
type DatabaseError = DatabaseError of string

type CreateProductError =
    | ValidationError of ValidationError
    | DatabaseError of DatabaseError
    