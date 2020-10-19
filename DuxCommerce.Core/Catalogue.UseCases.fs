namespace DuxCommerce.Catalogue

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
        let dto = product |> CreateProductRequest.toDomain
        match dto with
        | Ok _ ->
            product 
            |> DataAccess.updateProduct connString
        | Error m -> Error m
        