namespace DuxCommerce.Common

open System
open DuxCommerce.Common
open MongoDB.Driver

module MongoRepoAdapter = 

    let repoAdapter repoFn=
        try
            let func (appConfig:AppSettings) =

                let client = MongoClient(appConfig.ConnectionString)
                let database = client.GetDatabase(appConfig.DatabaseName)
                
                Ok (repoFn database)
                
            ConfigReader.ConfigReader func
        with
            | :? Exception as ex ->
                Error ex
                |> CustomError.mapInternalServer
                |> ConfigReader.retn 