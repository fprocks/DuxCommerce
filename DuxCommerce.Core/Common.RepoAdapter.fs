namespace DuxCommerce.Core.Common

open System
open System.Data.SqlClient
open DuxCommerce.Common
open RepoDb

module RepoAdapter = 

    let repoAdapter1 connString repoFn x =
        try
            ( use connection = new SqlConnection(connString)
              connection.EnsureOpen() |> ignore
              Ok (repoFn connection x)
            )
        with
            | :? Exception as ex -> Error ex |> CustomError.mapInternalServer
            
    let repoAdapter2 connString repoFn x y =
        try
            ( use connection = new SqlConnection(connString)
              connection.EnsureOpen() |> ignore
              Ok (repoFn connection x y)
            )            
        with
            | :? Exception as ex -> Error ex |> CustomError.mapInternalServer
            
    let repoAdapter3 connString repoFn x y z =
        try
            ( use connection = new SqlConnection(connString)
              connection.EnsureOpen() |> ignore
              Ok (repoFn connection x y z)
            )    
        with
            | :? Exception as ex -> Error ex |> CustomError.mapInternalServer                