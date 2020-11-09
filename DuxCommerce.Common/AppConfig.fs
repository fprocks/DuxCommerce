namespace DuxCommerce.Common

type ConfigInfo = {
    ConnectionString : string
}

type AppConfig () =
    static let mutable appConfig = {
        ConnectionString = ""
    }        
    member this.ConnectionString = 
        appConfig.ConnectionString
        
    member this.initialize (value:ConfigInfo) = 
        appConfig <- value
        