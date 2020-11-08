namespace DuxCommerce.Adapters.HttpControllers

open DuxCommerce.Common
open Microsoft.AspNetCore.Mvc

type DuxControllerBase () =
    inherit ControllerBase()
    
    member this.Convert(error: CustomError) : IActionResult =
        match error.Error with
        | Validation (ValidationError m) -> base.BadRequest(m) :> _
        | _ -> base.Problem() :> _ 