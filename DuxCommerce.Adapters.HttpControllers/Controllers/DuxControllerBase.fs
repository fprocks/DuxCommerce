namespace DuxCommerce.Adapters.HttpControllers

open DuxCommerce.Common
open Microsoft.AspNetCore.Mvc

type DuxControllerBase () =
    inherit ControllerBase()
    
    member this.Convert(error: CustomError) : IActionResult =
        match error.Error with
        | Validation e -> base.BadRequest() :> _
        | DataIntegrity e -> base.Problem() :> _
        | InternalServer e -> base.Problem() :> _
        | RemoteService e -> base.Problem() :> _