namespace DuxCommerce.Common

type AppConfig = {
    ConnectionString : string
}

type ConfigClient () =
    static let mutable appConfig = {
        ConnectionString = ""
    }        
    member this.ConnectionString = 
        appConfig.ConnectionString
        
    member this.Init (value:AppConfig) = 
        appConfig <- value
        
type ConfigReader<'a> = ConfigReader of (ConfigClient -> 'a)
    
module ConfigReader =

    let runReader (ConfigReader action) configClient =
        let resultOfAction = action configClient
        resultOfAction

    let map f reader = 
        let newAction configClient = 
            let x = runReader reader configClient 
            f x
        ConfigReader newAction

    let retn x =
        let newAction configClient =
            x
        ConfigReader newAction

    let apply fReader xReader =
        let newAction configClient =  
            let f = runReader fReader configClient
            let x = runReader xReader configClient
            f x
        ConfigReader newAction

    let bind f xAction = 
        let newAction configClient = 
            let x = runReader xAction configClient 
            runReader (f x) configClient 
        ConfigReader newAction

    let execute configReader = 
        let configClient = ConfigClient()
        runReader configReader configClient 