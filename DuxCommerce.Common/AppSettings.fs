namespace DuxCommerce.Common

open Microsoft.Extensions.Configuration

[<CLIMutable>]
type DatabaseSettings = {
    ConnectionString : string
}

module DatabaseSettings = 

    let FromFile (configFile:string) = 
        let mutable dbSettings = {ConnectionString = ""}
    
        let config = ConfigurationBuilder()
                        .AddJsonFile(configFile)
                        .Build()

        config.Bind("DatabaseSettings", dbSettings)

        dbSettings

type AppSettings () =
    static let mutable DbSettings = {ConnectionString = ""}

    member this.ConnectionString = 
        DbSettings.ConnectionString
        
    member this.SetDbSettings (value:DatabaseSettings) = 
        DbSettings <- value
        