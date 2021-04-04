namespace DuxCommerce.Core.Catalogue.Dto

open DuxCommerce.Core.Catalogue.InternalTypes
open DuxCommerce.Common
open DuxCommerce.Core.Catalogue.SimpleTypes
open DuxCommerce.Core.Catalogue.PublicTypes

module ProductDto =

    let internal toDomain productDto :Result<Product, CustomError> =
        result {
            let! name = String255.create "Name" productDto.Name
            let! productType = ProductType.create productDto.ProductType
            let! sku = String255.create "SKU" productDto.SKU
            let! barcode = String255.create "Barcode" productDto.Barcode
            let! outOfStockRule = OutOfStockRule.create productDto.OutOfStockRule
            return {
                ProductId = ProductId productDto.Id
                Name = name
                Description = productDto.Description
                Price = SalePrice productDto.Price
                Retail = RetailPrice productDto.Retail
                Cost = ProductCost productDto.Cost
                Length = Length productDto.Length
                Width = Width productDto.Width
                Height = Height productDto.Height
                Weight = Weight productDto.Weight
                ProductType = productType
                SKU = sku
                Barcode = barcode
                TrackInventory = productDto.TrackInventory
                OutOfStockRule = outOfStockRule
              }
            } |> CustomError.mapValidation
        
    let fromDomain product :ProductDto = {
            Id = ProductId.value product.ProductId
            Name = String255.value product.Name
            Description = product.Description
            Price = SalePrice.value product.Price
            Retail = RetailPrice.value product.Retail
            Cost = ProductCost.value product.Cost
            Length = Length.value product.Length
            Width = Width.value product.Width
            Height = Height.value product.Height
            Weight = Weight.value product.Weight
            ProductType = ProductType.value product.ProductType
            SKU = String255.value product.SKU
            Barcode = String255.value product.Barcode
            TrackInventory = product.TrackInventory
            OutOfStockRule = OutOfStockRule.value product.OutOfStockRule
            }        
 