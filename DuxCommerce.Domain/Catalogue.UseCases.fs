namespace DuxCommerce.Catalogue

open DuxCommerce.Common

module UseCases =     
    let createProduct connString request =
        let dto = request |> CreateProductRequest.toDomain
        match dto with
        | Ok _ ->
            ProductDto request 
            |> DataAccess.createProduct connString
        | Error m -> Error m
    