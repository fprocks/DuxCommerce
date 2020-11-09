namespace DuxCommerce.Common

open DuxCommerce.Common.ConfigReader

type ReaderResult<'Success, 'Failure> = ConfigReader<Result<'Success,'Failure>>

module ReaderResult = 
    let map f =
        map (Result.map f)
        
    let retn x =
        retn (Result.retn x)
        
    let mapError f (x:ReaderResult<_,_>) : ReaderResult<_,_> =
        ConfigReader.map (Result.mapError f) x
        
    let apply fReaderResult xReaderResult =
        let newAction configClient =
            let fResult = runReader fReaderResult configClient 
            let xResult = runReader xReaderResult configClient 
            Result.apply fResult xResult
            
        ConfigReader newAction

    let bind f xActionResult = 
        let newAction configClient = 
            let xResult = runReader xActionResult configClient 
            let yAction = 
                match xResult with
                | Result.Ok x ->
                    f x
                | Result.Error errs ->
                    (Result.Error errs) |> ConfigReader.retn
                    
            runReader yAction configClient 
            
        ConfigReader newAction
        
[<AutoOpen>]
module ReaderResultComputationExpression = 

    type ReaderResultBuilder() = 
        member __.Return(x) = ReaderResult.retn x
        member __.Bind(x, f) = ReaderResult.bind f x

        member __.ReturnFrom(x) = x
        member this.Zero() = this.Return ()

        member __.Delay(f) = f
        member __.Run(f) = f()

        member this.While(guard, body) =
            if not (guard()) 
            then this.Zero() 
            else this.Bind( body(), fun () -> 
                this.While(guard, body))  

        member this.TryWith(body, handler) =
            try this.ReturnFrom(body())
            with e -> handler e

        member this.TryFinally(body, compensation) =
            try this.ReturnFrom(body())
            finally compensation() 

        member this.Using(disposable:#System.IDisposable, body) =
            let body' = fun () -> body disposable
            this.TryFinally(body', fun () -> 
                match disposable with 
                    | null -> () 
                    | disp -> disp.Dispose())

        member this.For(sequence:seq<_>, body) =
            this.Using(sequence.GetEnumerator(),fun enum -> 
                this.While(enum.MoveNext, 
                    this.Delay(fun () -> body enum.Current)))

        member this.Combine (a,b) = 
            this.Bind(a, fun () -> b())

    let readerResult = ReaderResultBuilder()  