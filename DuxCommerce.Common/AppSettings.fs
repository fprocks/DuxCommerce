namespace DuxCommerce.Common

open Microsoft.Extensions.Configuration

[<CLIMutable>]
type SqlServerSettings = {
    ConnectionString : string
}

module SqlServerSettings = 
    let FromFile (configFile:string) = 
        let mutable settings = {ConnectionString = ""}
    
        let config = ConfigurationBuilder()
                        .AddJsonFile(configFile)
                        .Build()
        config.Bind("SqlServerSettings", settings)

        settings

[<CLIMutable>]        
type MongoSettings = {
    ConnectionString : string
    DatabaseName : string
}

module MongoSettings =

    let FromFile (configFile:string) = 
        let mutable settings = {ConnectionString = ""; DatabaseName = ""}

        let config = ConfigurationBuilder()
                        .AddJsonFile(configFile)
                        .Build()
        config.Bind("MongoSettings", settings)

        settings

type AppSettings () =
    static let mutable SqlSettings = {ConnectionString = ""}
    static let mutable MongoSettings = {ConnectionString = ""; DatabaseName = ""}

    member this.GetSqlSettings = 
        SqlSettings
    member this.SetSqlSettings (value:SqlServerSettings) = 
        SqlSettings <- value

    member this.GetMongoSettings = 
        MongoSettings       
    member this.SetMongoSettings (value:MongoSettings) = 
        MongoSettings <- value
        