namespace DuxCommerce.Adapters.Http

open DuxCommerce.Common
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open DuxCommerce.Core.Taxes.PublicTypes
open DuxCommerce.Core.Taxes

[<ApiController>]
[<Route("api/[controller]")>]
type TaxRatesController (logger : ILogger<TaxRatesController>) =
    inherit DuxControllerBase()

    [<HttpPost>]
    member this.Post(request: TaxRateDto) : IActionResult =
        let result = ConfigReader.execute (TaxRateUseCases.createRate request)
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)