namespace DuxCommerce.Catalogue.Ports

open DuxCommerce.Catalogue.PublicTypes
open DuxCommerce.Common

// Input port
type CreateProductUseCase = ProductDto -> Result<ProductDto, CustomError>
type GetProductUseCase = int64 -> Result<ProductDto, CustomError>
type UpdateProductUseCase = int64 -> ProductDto -> Result<ProductDto, CustomError>


// Output port
type CreateProduct = ProductDto -> Result<int64, CustomError>
type GetProduct = int64 -> Result<ProductDto, CustomError>
type UpdateProduct = int64 -> ProductDto -> Result<unit, CustomError>  
