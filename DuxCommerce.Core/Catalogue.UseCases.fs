namespace DuxCommerce.Catalogue

open DuxCommerce.Catalogue.Ports
open DuxCommerce.Common
open DuxCommerce.Catalogue.Dto

module UseCases =     

    let createProduct :CreateProductUseCase =            
        fun productDto ->
            readerResult {
                let! product = productDto |> ProductDto.toDomain |> ConfigReader.retn
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
                let! product = productDto |> ProductDto.toDomain |> ConfigReader.retn
                do! ProductRepo.updateProduct id productDto
                return! ProductRepo.getProduct id
            }