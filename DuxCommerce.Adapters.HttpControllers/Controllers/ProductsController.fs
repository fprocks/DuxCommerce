namespace DuxCommerce.Adapters.HttpControllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging

[<ApiController>]
[<Route("api/v1/[controller]")>]
type ProductsController (logger : ILogger<ProductsController>) =
    inherit ControllerBase()

    [<HttpGet>]
    member this.Get() : string =
        "test"
        
//    member this.Post(): CreateProductResponse =
//        ""