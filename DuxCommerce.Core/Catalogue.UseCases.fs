namespace DuxCommerce.Catalogue

module UseCases =     
    let createProduct connString product =
        let model = product |> CreateProductRequest.toDomain
        match model with
        | Ok _ ->
            product 
            |> DataAccess.createProduct connString
        | Error m -> Error m
    
    let getProduct connString id =
        DataAccess.getProduct connString id
        
    let updateProduct connString id product =
        let model = product |> CreateProductRequest.toDomain
        match model with
        | Ok _ ->
            product 
            |> DataAccess.updateProduct connString id
        | Error m -> Error m
        