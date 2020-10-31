namespace DuxCommerce.Catalogue

open DuxCommerce.Catalogue.Dto
open DuxCommerce.Common
open DuxCommerce.Catalogue.SimpleTypes

module UseCases =     
    let createProduct connString info =
        result {
            let! product = info |> ProductInfo.toDomain
            let! info = product |> ProductRepo.createProduct connString
            return! ProductRepo.getProduct connString (ProductId.create info.Id) 
        }
    
    let getProduct connString productId =
        // Todo: improve to handle null value
        ProductRepo.getProduct connString productId
        
    let updateProduct connString id info =
        result {
            let! product = info |> ProductInfo.toDomain
            let productId = ProductId.create id
            do! ProductRepo.updateProduct connString productId product
            return! ProductRepo.getProduct connString productId
        }