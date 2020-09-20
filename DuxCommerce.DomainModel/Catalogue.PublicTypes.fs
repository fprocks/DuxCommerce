namespace DuxCommerce.DomainModel.Catalogue

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
    Shippable : int
    SKU : string
    Barcode : string
    TrackInventory : bool
    OutOfStock : int
}

//type CreateProductResponse = {
//}

type CreateProduct = CreateProductRequest -> unit
    