namespace DuxCommerce.Common

type ConfigReader<'a> = ConfigReader of (AppConfig -> 'a)
    
module ConfigReader =

    let runReader (ConfigReader action) appConfig =
        let resultOfAction = action appConfig
        resultOfAction

    let map f reader = 
        let newAction appConfig = 
            let x = runReader reader appConfig 
            f x
        ConfigReader newAction

    let retn x =
        let newAction appConfig =
            x
        ConfigReader newAction

    let apply fReader xReader =
        let newAction appConfig =  
            let f = runReader fReader appConfig
            let x = runReader xReader appConfig
            f x
        ConfigReader newAction

    let bind f xAction = 
        let newAction appConfig = 
            let x = runReader xAction appConfig 
            runReader (f x) appConfig 
        ConfigReader newAction

    let execute configReader = 
        let appConfig = AppConfig()
        runReader configReader appConfig 