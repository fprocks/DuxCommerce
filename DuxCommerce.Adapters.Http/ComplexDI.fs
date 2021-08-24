namespace ComplexDI

// https://bartoszsypytkowski.com/dealing-with-complex-dependency-injection-in-f/

type User = { Salt: string }

[<Interface>]
type ILogger =
    abstract Debug : string -> unit
    abstract Error : string -> unit

[<Interface>]
type ILog =
    abstract Logger : ILogger

module Log =
    let debug (env: #ILog) fmt = Printf.kprintf env.Logger.Debug fmt
    let error (env: #ILog) fmt = Printf.kprintf env.Logger.Error fmt

    let live: ILogger =
        { new ILogger with
            member this.Debug s = ()
            member this.Error s = () }

[<Interface>]
type IDatabase =
    abstract Query : string * 'i -> 'o
    abstract Execute : string * 'i -> unit

[<Interface>]
type IDb =
    abstract Database : IDatabase

module Db =
    let fetchUser (env: #IDb) userId =
        env.Database.Query("", {| userId = userId |})

    let updateUser (env: #IDb) user = env.Database.Execute("", user)
    let live: IDatabase = failwith "Not implemented yet"


[<Struct>]
type AppEnv =
    interface ILog with
        member _.Logger = Log.live

    interface IDb with
        member _.Database = Db.live

module Main =
    let changePass env req =
        let user = Db.fetchUser env 1
        Db.updateUser env { user with Salt = "" }
        Log.debug env "Changed password for user"

    let foo env = // env :> IDb and env :> ILog
        let user = Db.fetchUser env 123
        Log.debug env "User: %A" user

    foo (AppEnv())
