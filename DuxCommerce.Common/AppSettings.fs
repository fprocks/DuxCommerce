namespace DuxCommerce.Common

open Microsoft.Extensions.Configuration

[<CLIMutable>]
type AppSettings =
    { ConnectionString: string
      DatabaseName: string }

module AppSettings =

    let fromFile (configFile: string) =
        let mutable settings =
            { ConnectionString = ""
              DatabaseName = "" }

        let config =
            ConfigurationBuilder()
                .AddJsonFile(configFile)
                .Build()

        config.Bind("MongoSettings", settings)

        settings
