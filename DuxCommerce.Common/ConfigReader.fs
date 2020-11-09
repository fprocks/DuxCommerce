namespace DuxCommerce.Common

module ConfigReader =
    
    type ConfigClient() =
        static let mutable appConfig = {
            ConnectionString = ""
        }        
        member this.ConnectionString = 
            appConfig.ConnectionString
            
        member this.Set (value:AppConfig) = 
            appConfig <- value          

    type ConfigReader<'a> = ConfigReader of (ConfigClient -> 'a)

    let run configClient (ConfigReader action) =
        let resultOfAction = action configClient
        resultOfAction

    let map f action = 
        let newAction configClient = 
            let x = run configClient action
            f x
        ConfigReader newAction

    let retn x =
        let newAction configClient =
            x
        ConfigReader newAction

    let apply fAction xAction =
        let newAction configClient = 
            let f = run configClient fAction
            let x = run configClient xAction
            f x
        ConfigReader newAction

    let bind f xAction = 
        let newAction configClient = 
            let x = run configClient xAction
            run configClient (f x)
        ConfigReader newAction

    let execute configReader = 
        let configClient = ConfigClient()
        run configClient configReader