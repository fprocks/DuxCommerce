namespace DuxCommerce.Catalogue

open DuxCommerce.Catalogue.Ports
open DuxCommerce.Common
open DuxCommerce.Catalogue.Dto

module UseCases =     

    let createProduct connString :CreateProductUseCase   =            
        fun info ->
            result {
                let! product = info |> ProductInfo.toDomain
                let! id = info |> ProductRepo.createProduct connString
                return! ProductRepo.getProduct connString id 
            }
    
    let getProduct connString :GetProductUseCase =
        fun id ->
            result {                
                // Todo: improve to handle null value
                return! ProductRepo.getProduct connString id
            }
        
    let updateProduct connString :UpdateProductUseCase =
        fun id info ->
            result {
                let! product = info |> ProductInfo.toDomain
                do! ProductRepo.updateProduct connString id info
                return! ProductRepo.getProduct connString id
            }