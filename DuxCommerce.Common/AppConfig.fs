namespace DuxCommerce.Common

type Config = {
    ConnectionString : string
}

type AppConfig () =
    static let mutable config = {ConnectionString = ""}    
    member this.ConnectionString = 
        config.ConnectionString
        
    member this.initialize (value:Config) = 
        config <- value
        