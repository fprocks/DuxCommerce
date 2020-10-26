namespace DuxCommerce.Catalogue

open DuxCommerce.Catalogue.Dto
open DuxCommerce.Common

module UseCases =     
    let createProduct connString productInfo =
        result {
            let! model = productInfo |> Product.toDomain
            let! product = productInfo |> CatalogueDb.createProduct connString
            return! CatalogueDb.getProduct connString product.Id
        }
    
    let getProduct connString id =
        CatalogueDb.getProduct connString (ProductId.value id)
        
    let updateProduct connString id productInfo =
        result {
            let! product = productInfo |> Product.toDomain
            do! CatalogueDb.updateProduct connString id productInfo
            return! CatalogueDb.getProduct connString id
        }