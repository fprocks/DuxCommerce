namespace DuxCommerce.Core.Catalogue

open DuxCommerce.Core.Catalogue.PublicTypes
open DuxCommerce.Common
open DuxCommerce.Core.Catalogue.Dto
open DuxCommerce.Core.Catalogue.MongoRepos

type CreateProductUseCase = ProductDto -> ConfigReader<Result<ProductDto, CustomError>>
type GetProductUseCase = string -> ConfigReader<Result<ProductDto, CustomError>>
type UpdateProductUseCase = string -> ProductDto -> ConfigReader<Result<ProductDto, CustomError>>

module ProductUseCases =     

    let createProduct :CreateProductUseCase =            
        fun productDto ->
            readerResult {
                let! product = 
                    productDto 
                    |> ProductDto.toDomain 
                    |> ConfigReader.retn
                let! id = productDto |> ProductRepo.createProduct
                return! ProductRepo.getProduct id 
            }
    
    let getProduct :GetProductUseCase =
        fun id ->
            readerResult {                
                // Todo: improve to handle null value
                return! ProductRepo.getProduct id
            }
        
    let updateProduct :UpdateProductUseCase =
        fun id productDto ->
            readerResult {
                let! product = 
                    productDto 
                    |> ProductDto.toDomain 
                    |> ConfigReader.retn
                do! ProductRepo.updateProduct id productDto
                return! ProductRepo.getProduct id
            }