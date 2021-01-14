namespace DuxCommerce.Adapters.Http

open DuxCommerce.Common
open DuxCommerce.Shipping
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open DuxCommerce.Shipping.PublicTypes

[<ApiController>]
[<Route("api/[controller]")>]
type ShippingProfilesController (logger : ILogger<ShippingProfilesController>) =
    inherit DuxControllerBase()

    [<HttpGet("default")>]
    member this.Get() : IActionResult=
        let result = ConfigReader.execute (ShippingProfileUseCases.getDefaultProfile ())
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)

    [<HttpPost>]
    member this.Post(request: ShippingProfileDto) : IActionResult =
        let result = ConfigReader.execute (ShippingProfileUseCases.createProfile request)
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)