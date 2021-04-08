namespace DuxCommerce.Common

type ConfigReader<'a> = ConfigReader of (AppSettings -> 'a)
    
module ConfigReader =

    let runReader (ConfigReader func) appConfig =
        func appConfig

    let map f reader = 
        let newFunc appConfig = 
            let x = runReader reader appConfig 
            f x
        ConfigReader newFunc

    let retn x =
        let newFunc appConfig =
            x
        ConfigReader newFunc

    let apply fReader xReader =
        let newFunc appConfig =  
            let f = runReader fReader appConfig
            let x = runReader xReader appConfig
            f x
        ConfigReader newFunc

    let bind f xAction = 
        let newFunc appConfig = 
            let x = runReader xAction appConfig 
            runReader (f x) appConfig 
        ConfigReader newFunc

    let execute configReader =
        let config = MongoSettings.FromFile "appsettings.json"
        runReader configReader config 