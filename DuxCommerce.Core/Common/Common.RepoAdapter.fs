namespace DuxCommerce.Core.Common

open System
open System.Data.SqlClient
open DuxCommerce.Common
open RepoDb

module RepoAdapter = 

    let repoAdapter repoFn=
        try
            let func (appConfig:AppSettings) =
                let connString = appConfig.ConnectionString
                ( use connection = new SqlConnection(connString)
                  connection.EnsureOpen() |> ignore
                  Ok (repoFn connection)
                )
                
            ConfigReader.ConfigReader func
        with
            | :? Exception as ex ->
                Error ex
                |> CustomError.mapInternalServer
                |> ConfigReader.retn 