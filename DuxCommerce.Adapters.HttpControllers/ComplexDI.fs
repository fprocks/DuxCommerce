namespace ComplexDI

// https://bartoszsypytkowski.com/dealing-with-complex-dependency-injection-in-f/

[<Interface>]
type ILogger =
    abstract Debug: string -> unit
    abstract Error: string -> unit 

module Log =
    let debug (env: #ILogger) fmt = Printf.kprintf env.Debug fmt
    let error (env: #ILogger) fmt = Printf.kprintf env.Error fmt
    let live : ILogger = failwith "Not implemented yet"


[<Interface>]
type IDatabase =
    abstract Query: string * 'i -> 'o
    abstract Execute: string * 'i -> unit

module Db = 
    let fetchUser (env: #IDatabase) userId = env.Query("", {| userId = userId |})
    let updateUser (env: #IDatabase) user = env.Execute("", user)
    let live : IDatabase = failwith "Not implemented yet"


[<Struct>]
type AppEnv = 
    interface ILogger with
        member this.Debug s = Log.live.Debug s
        member this.Error s = Log.live.Error s
        
    interface IDatabase with
        member this.Query (s, i) = Db.live.Query (s, i)
        member this.Execute (s, i) = Db.live.Execute (s, i)
        
module Main =    
    let foo env = // env :> IDb and env :> ILog
        let user = Db.fetchUser env 123
        Log.debug env "User: %A" user
        

    foo (AppEnv())        
        