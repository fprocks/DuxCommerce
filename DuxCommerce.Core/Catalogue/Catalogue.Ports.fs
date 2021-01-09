namespace DuxCommerce.Catalogue.Ports

open DuxCommerce.Catalogue.PublicTypes
open DuxCommerce.Common

// Input port
type CreateProductUseCase = ProductDto -> ConfigReader<Result<ProductDto, CustomError>>
type GetProductUseCase = string -> ConfigReader<Result<ProductDto, CustomError>>
type UpdateProductUseCase = string -> ProductDto -> ConfigReader<Result<ProductDto, CustomError>>


// Output port
type CreateProduct = ProductDto -> ConfigReader<Result<string, CustomError>>
type GetProduct = string -> ConfigReader<Result<ProductDto, CustomError>>
type UpdateProduct = string -> ProductDto -> ConfigReader<Result<unit, CustomError>>
