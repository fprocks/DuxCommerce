open System
open PlaceOrder

[<EntryPoint>]
let main argv =
    insertProduct 2 "F#" 100M |> ignore

    let products = getProduct "F#"
    match products with
    | [] -> printfn "nothing"
    | head :: _  -> printf "%s" head.Name

    0
