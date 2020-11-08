namespace DuxCommerce.Catalogue.PublicTypes

[<CLIMutable>]
type ProductDto = {
    Id: int64
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

type ValidationError = ValidationError of string
type DatabaseError = DatabaseError of string

type CreateProductError =
    | ValidationError of ValidationError
    | DatabaseError of DatabaseError
    