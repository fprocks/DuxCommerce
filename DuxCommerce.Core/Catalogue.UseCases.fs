namespace DuxCommerce.Catalogue

open DuxCommerce.Catalogue.Dto
open DuxCommerce.Common

module UseCases =     
    let createProduct connString info =
        result {
            let! product = info |> ProductInfo.toDomain
            let! info = product |> CatalogueDb.createProduct connString
            return! CatalogueDb.getProduct connString (ProductId.create info.Id) 
        }
    
    let getProduct connString productId =
        // Todo: improve to handle null value
        CatalogueDb.getProduct connString productId
        
    let updateProduct connString id info =
        result {
            let! product = info |> ProductInfo.toDomain
            let productId = ProductId.create id
            do! CatalogueDb.updateProduct connString productId product
            return! CatalogueDb.getProduct connString productId
        }