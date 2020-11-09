namespace DuxCommerce.Core.Common

open System
open System.Data.SqlClient
open DuxCommerce.Common
open RepoDb

module RepoAdapter = 

    let repoAdapter1 repoFn x =
        try
            let action (appConfig:AppConfig) =
                let connString = appConfig.ConnectionString
                ( use connection = new SqlConnection(connString)
                  connection.EnsureOpen() |> ignore
                  Ok (repoFn connection x)
                )
                
            ConfigReader.ConfigReader action
        with
            | :? Exception as ex ->
                Error ex
                |> CustomError.mapInternalServer
                |> ConfigReader.retn
            
    let repoAdapter2 repoFn x y =
        try
            let action (appConfig:AppConfig) =
                let connString = appConfig.ConnectionString
                ( use connection = new SqlConnection(connString)
                  connection.EnsureOpen() |> ignore
                  Ok (repoFn connection x y)
                )
                
            ConfigReader.ConfigReader action
        with
            | :? Exception as ex ->
                Error ex
                |> CustomError.mapInternalServer
                |> ConfigReader.retn
            
    let repoAdapter3 repoFn x y z =
        try
            let action (appConfig:AppConfig) =
                let connString = appConfig.ConnectionString
                ( use connection = new SqlConnection(connString)
                  connection.EnsureOpen() |> ignore
                  Ok (repoFn connection x y z)
                )
                
            ConfigReader.ConfigReader action
        with
            | :? Exception as ex ->
                Error ex
                |> CustomError.mapInternalServer
                |> ConfigReader.retn           