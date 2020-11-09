namespace DuxCommerce.Common

type Config = {
    ConnectionString : string
}

type AppConfig private() =
    let mutable config = {ConnectionString = ""}
    static let instance = AppConfig()
    
    static member Instance = instance
    member this.ConnectionString = 
        config.ConnectionString        
    member this.initialize (value:Config) = 
        config <- value
        