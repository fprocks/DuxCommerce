namespace DuxCommerce.Catalogue.Ports

open DuxCommerce.Catalogue.InternalTypes
open DuxCommerce.Catalogue.PublicTypes
open DuxCommerce.Catalogue.SimpleTypes
open DuxCommerce.Common

// Input port
type CreateProductUseCase = ProductInfo -> Result<ProductInfo, CustomError>
type GetProductUseCase = int64 -> Result<ProductInfo, CustomError>
type UpdateProductUseCase = int64 -> ProductInfo -> Result<ProductInfo, CustomError>


// Output port
type CreateProduct = Product -> Result<int64, CustomError>
type GetProduct = ProductId -> Result<ProductInfo, CustomError>
type UpdateProduct = ProductId -> Product -> Result<unit, CustomError>  
