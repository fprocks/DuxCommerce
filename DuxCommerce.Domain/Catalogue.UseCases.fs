namespace DuxCommerce.Catalogue

open DuxCommerce.Common

module UseCases =     
    let createProduct connString product =
        let dto = product |> CreateProductRequest.toDomain
        match dto with
        | Ok _ ->
            product 
            |> DataAccess.createProduct connString
        | Error m -> Error m
    
    let getProduct connString id =
        DataAccess.getProduct connString id
        
    let updateProduct connString product =
        DataAccess.updateProduct connString product