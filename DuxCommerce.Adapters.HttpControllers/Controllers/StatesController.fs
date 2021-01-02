namespace DuxCommerce.Adapters.HttpControllers

open DuxCommerce.Common
open DuxCommerce.Settings
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging

[<ApiController>]
[<Route("api/[controller]")>]
type StatesController (logger : ILogger<StatesController>) =
    inherit DuxControllerBase()

    [<HttpGet("default")>]
    member this.Get() : IActionResult=
        let result = ConfigReader.execute (ShippingProfileUseCases.getDefaultProfile ())
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)
