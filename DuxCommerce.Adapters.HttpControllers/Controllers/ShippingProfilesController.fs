namespace DuxCommerce.Adapters.HttpControllers

open DuxCommerce.Common
open DuxCommerce.Settings
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open DuxCommerce.Settings.PublicTypes

[<ApiController>]
[<Route("api/[controller]")>]
type ShippingProfilesController (logger : ILogger<ShippingProfilesController>) =
    inherit DuxControllerBase()

    [<HttpGet("default")>]
    member this.Get() : IActionResult=
        let result = ConfigReader.execute (UseCases.getDefaultShippingProfile ())
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)

    [<HttpPost>]
    member this.Post(request: ShippingProfileDto) : IActionResult =
        let result = ConfigReader.execute (UseCases.createShippingProfile request)
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)