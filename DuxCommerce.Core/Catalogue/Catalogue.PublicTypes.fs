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
    ProductType : string
    SKU : string
    Barcode : string
    TrackInventory : bool
    OutOfStockRule : string
}