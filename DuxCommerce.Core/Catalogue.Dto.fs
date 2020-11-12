namespace DuxCommerce.Catalogue.Dto

open DuxCommerce.Catalogue.InternalTypes
open DuxCommerce.Common
open DuxCommerce.Catalogue.SimpleTypes
open DuxCommerce.Catalogue.PublicTypes

module ProductDto =

    let internal toDomain' (productDto: ProductDto) :Result<Product, string> =
        result {
            let! name = String255.create "Name" productDto.Name
            let! shippingType = ShippingType.create productDto.ShippingType
            let! sku = String255.create "SKU" productDto.SKU
            let! barcode = String255.create "Barcode" productDto.Barcode
            let! outOfStockRule = OutOfStockRule.create productDto.OutOfStockRule
            return {
                Id = ProductId productDto.Id
                Name = name
                Description = productDto.Description
                Price = SalePrice productDto.Price
                Retail = RetailPrice productDto.Retail
                Cost = ProductCost productDto.Cost
                Length = Length productDto.Length
                Width = Width productDto.Width
                Height = Height productDto.Height
                Weight = Weight productDto.Weight
                ShippingType = shippingType
                SKU = sku
                Barcode = barcode
                TrackInventory = productDto.TrackInventory
                OutOfStockRule = outOfStockRule
              }
        }

    let toDomain (productDto: ProductDto) :Result<Product, CustomError> =
        toDomain' productDto
        |> CustomError.mapValidation
        
    let fromDomain (product:Product) :ProductDto =
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
 