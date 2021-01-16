namespace DuxCommerce.Adapters.Http

open DuxCommerce.Common
open DuxCommerce.Core.Shipping
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open DuxCommerce.Core.Shipping.PublicTypes

[<ApiController>]
[<Route("api/[controller]")>]
type ShippingOriginsController (logger : ILogger<ShippingOriginsController>) =
    inherit DuxControllerBase()

    [<HttpGet>]
    member this.Get([<FromQuery>] originIds) : IActionResult=
        let result = ConfigReader.execute (ShippingOriginUseCases.getOrigins originIds)
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)

    [<HttpPost>]
    member this.Post(request: AddressDto) : IActionResult =
        let result = ConfigReader.execute (ShippingOriginUseCases.createOrigin request)
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)
