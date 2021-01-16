namespace DuxCommerce.Core.Common

open System
open DuxCommerce.Common
open MongoDB.Driver

module MongoRepoAdapter = 

    let repoAdapter repoFn=
        try
            let func (appConfig:AppSettings) =
                let settings = appConfig.GetMongoSettings

                let client = MongoClient(settings.ConnectionString)
                let database = client.GetDatabase(settings.DatabaseName)
                
                Ok (repoFn database)
                
            ConfigReader.ConfigReader func
        with
            | :? Exception as ex ->
                Error ex
                |> CustomError.mapInternalServer
                |> ConfigReader.retn 