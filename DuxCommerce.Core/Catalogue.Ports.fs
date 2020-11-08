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
type CreateProduct = ProductInfo -> Result<int64, CustomError>
type GetProduct = int64 -> Result<ProductInfo, CustomError>
type UpdateProduct = int64 -> ProductInfo -> Result<unit, CustomError>  
