namespace DuxCommerce.Core.Catalogue

open DuxCommerce.Core.Catalogue.PublicTypes
open DuxCommerce.Common
open DuxCommerce.Core.Catalogue.Dto
open DuxCommerce.Core.Catalogue.MongoRepos

module ProductUseCases =    

    type CreateProductUseCase = ProductDto -> ConfigReader<Result<ProductDto, CustomError>>
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

    type GetProductUseCase = string -> ConfigReader<Result<ProductDto, CustomError>>
    let getProduct :GetProductUseCase =
        fun id ->
            readerResult {                
                // Todo: improve to handle null value
                return! ProductRepo.getProduct id
            }

    type UpdateProductUseCase = string -> ProductDto -> ConfigReader<Result<ProductDto, CustomError>>    
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