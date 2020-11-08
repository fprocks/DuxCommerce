namespace DuxCommerce.Catalogue

open DuxCommerce.Catalogue.Ports
open DuxCommerce.Common
open DuxCommerce.Catalogue.Dto

module UseCases =     

    let createProduct connString :CreateProductUseCase =            
        fun productDto ->
            result {
                let! product = productDto |> ProductDto.toDomain
                let! id = productDto |> ProductRepo.createProduct connString
                return! ProductRepo.getProduct connString id 
            }
    
    let getProduct connString :GetProductUseCase =
        fun id ->
            result {                
                // Todo: improve to handle null value
                return! ProductRepo.getProduct connString id
            }
        
    let updateProduct connString :UpdateProductUseCase =
        fun id productDto ->
            result {
                let! product = productDto |> ProductDto.toDomain
                do! ProductRepo.updateProduct connString id productDto
                return! ProductRepo.getProduct connString id
            }