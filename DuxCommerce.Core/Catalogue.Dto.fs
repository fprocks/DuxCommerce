namespace DuxCommerce.Catalogue.Dto

open DuxCommerce.Catalogue.InternalTypes
open DuxCommerce.Common
open DuxCommerce.Catalogue.SimpleTypes
open DuxCommerce.Catalogue.PublicTypes

module ProductInfo =
    let internal toDomain' (productInfo: ProductInfo) :Result<Product, string> =
        result {
            let! name = String255.create "Name" productInfo.Name
            let! shippingType = ShippingType.create productInfo.ShippingType
            let! sku = String255.create "SKU" productInfo.SKU
            let! barcode = String255.create "Barcode" productInfo.Barcode
            let! outOfStockRule = OutOfStockRule.create productInfo.OutOfStockRule
            return {
                Id = ProductId productInfo.Id
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

    let toDomain (productInfo: ProductInfo) :Result<Product, CustomError> =
        toDomain' productInfo |> CustomError.mapValidation
        
    let fromDomain (product:Product) :ProductInfo =
        {
            Id = ProductId.value product.Id
            Name = String255.value product.Name
            Description = product.Description
            Price = SalePrice.value product.Price
            Retail = RetailPrice.value product.Retail
            Cost = ProductCost.value product.Cost
            Length = Length.value product.Length
            Width = Width.value product.Width
            Height = Height.value product.Height
            Weight = Weight.value product.Weight
            ShippingType = ShippingType.value product.ShippingType
            SKU = String255.value product.SKU
            Barcode = String255.value product.Barcode
            TrackInventory = product.TrackInventory
            OutOfStockRule = OutOfStockRule.value product.OutOfStockRule
        }        
 