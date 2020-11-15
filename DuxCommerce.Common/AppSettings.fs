namespace DuxCommerce.Common

[<CLIMutable>]
type DatabaseSettings = {
    ConnectionString : string
}

type AppSettings () =
    static let mutable DbSettings = {ConnectionString = ""}

    member this.ConnectionString = 
        DbSettings.ConnectionString
        
    member this.initialize (value:DatabaseSettings) = 
        DbSettings <- value
        