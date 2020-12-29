namespace DuxCommerce.Adapters.HttpControllers

open DuxCommerce.Common
open DuxCommerce.Settings
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging

[<ApiController>]
[<Route("api/[controller]")>]
type ShippingProfileController (logger : ILogger<ShippingProfileController>) =
    inherit DuxControllerBase()

    [<HttpGet("default")>]
    member this.Get() : IActionResult=
        let result = ConfigReader.execute (UseCases.getDefaultShippingProfile ())
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)
