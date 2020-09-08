namespace DuxCommerce.Adapters.HttpControllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging

[<ApiController>]
[<Route("[controller]")>]
type ProductController (logger : ILogger<ProductController>) =
    inherit ControllerBase()

    [<HttpGet>]
    member this.Get() : string =
        "test"