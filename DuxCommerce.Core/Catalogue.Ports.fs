namespace DuxCommerce.Catalogue.Ports

open DuxCommerce.Catalogue.PublicTypes
open DuxCommerce.Common
open DuxCommerce.Common.ConfigReader

// Input port
type CreateProductUseCase = ProductDto -> ConfigReader<Result<ProductDto, CustomError>>
type GetProductUseCase = int64 -> ConfigReader<Result<ProductDto, CustomError>>
type UpdateProductUseCase = int64 -> ProductDto -> ConfigReader<Result<ProductDto, CustomError>>


// Output port
type CreateProduct = ProductDto -> ConfigReader<Result<int64, CustomError>>
type GetProduct = int64 -> ConfigReader<Result<ProductDto, CustomError>>
type UpdateProduct = int64 -> ProductDto -> ConfigReader<Result<unit, CustomError>>
