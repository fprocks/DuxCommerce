namespace DuxCommerce.Catalogue

open DuxCommerce.Catalogue.Ports
open DuxCommerce.Common
open DuxCommerce.Catalogue.Dto

module UseCases =     

    let createProduct createProduct getProduct :CreateProductUseCase   =            
        fun info ->
            result {
                let! product = info |> ProductInfo.toDomain
                let! id = info |> createProduct
                return! getProduct id 
            }
    
    let getProduct getProduct :GetProductUseCase =
        fun id ->
            result {                
                // Todo: improve to handle null value
                return! getProduct id
            }
        
    let updateProduct updateProduct getProduct :UpdateProductUseCase =
        fun id info ->
            result {
                let! product = info |> ProductInfo.toDomain
                do! updateProduct id info
                return! getProduct id
            }