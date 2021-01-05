namespace DuxCommerce.Core.Common

open System
open System.Data.SqlClient
open DuxCommerce.Common
open RepoDb

module SqlRepoAdapter = 

    let repoAdapter repoFn=
        try
            let func (appConfig:AppSettings) =
                let connString = appConfig.GetSqlSettings.ConnectionString
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