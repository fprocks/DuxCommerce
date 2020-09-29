namespace DuxCommerce.Adapters.HttpControllers

open DuxCommerce.Catalogue
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open CompositionRoot

[<ApiController>]
[<Route("api/v1/[controller]")>]
type ProductsController (logger : ILogger<ProductsController>) =
    inherit ControllerBase()

    [<HttpGet("{id}")>]
    member this.Get(id : int64) =
        getProduct id
        
    member this.Post(request: ProductInfo): Result<unit, string> =
        createProduct request