namespace DuxCommerce.Adapters.Http

open DuxCommerce.Common
open DuxCommerce.Payments
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open DuxCommerce.Payments.PublicTypes

[<ApiController>]
[<Route("api/[controller]")>]
type PaymentMethodsController (logger : ILogger<PaymentMethodsController>) =
    inherit DuxControllerBase()

    [<HttpPost>]
    member this.Post(request: PaymentMethodDto) : IActionResult =
        let result = ConfigReader.execute (PaymentMethodUseCases.createMethod request)
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)