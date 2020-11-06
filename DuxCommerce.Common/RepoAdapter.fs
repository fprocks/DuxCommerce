namespace DuxCommerce.Common

open System

module RepoAdapter = 

    let repoAdapter1  repoFn x =
        try
            Ok (repoFn x)
        with
            | :? Exception as ex -> Error ex |> CustomError.mapInternalServer
            
    let repoAdapter2  repoFn x y =
        try
            Ok (repoFn x y)
        with
            | :? Exception as ex -> Error ex |> CustomError.mapInternalServer
            
    let repoAdapter3  repoFn x y z =
        try
            Ok (repoFn x y z)
        with
            | :? Exception as ex -> Error ex |> CustomError.mapInternalServer                