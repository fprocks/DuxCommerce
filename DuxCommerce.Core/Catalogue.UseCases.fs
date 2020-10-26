namespace DuxCommerce.Catalogue

open DuxCommerce.Catalogue.Dto

module UseCases =     
    let createProduct connString product =
        let model = product |> Product.toDomain
        match model with
        | Ok _ ->
            product 
            |> CatalogueDb.createProduct connString
        | Error m -> Error m
    
    let getProduct connString id =
        CatalogueDb.getProduct connString (ProductId.value id)
        
    let updateProduct connString id product =
        let model = product |> Product.toDomain
        match model with
        | Ok _ ->
            product 
            |> CatalogueDb.updateProduct connString id
        | Error m -> Error m
        