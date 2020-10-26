namespace DuxCommerce.Catalogue.Dto

open DuxCommerce.Catalogue
open DuxCommerce.Common

module Product =
    let toDomain (productInfo: ProductInfo) :Result<Product, string> =
        result {
            let! name = String255.create "Name" productInfo.Name
            let! shippingType = ShippingType.create productInfo.ShippingType
            let! sku = String255.create "SKU" productInfo.SKU
            let! barcode = String255.create "Barcode" productInfo.Barcode
            let! outOfStockRule = OutOfStockRule.create productInfo.OutOfStockRule
            return {
                Id = ProductId (int64 0)
                Name = name
                Description = productInfo.Description
                Price = SalePrice productInfo.Price
                Retail = RetailPrice productInfo.Retail
                Cost = ProductCost productInfo.Cost
                Length = Length productInfo.Length
                Width = Width productInfo.Width
                Height = Height productInfo.Height
                Weight = Weight productInfo.Weight
                ShippingType = shippingType
                SKU = sku
                Barcode = barcode
                TrackInventory = productInfo.TrackInventory
                OutOfStockRule = outOfStockRule
                }
            }