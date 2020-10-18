namespace DuxCommerce.Adapters.HttpControllers

open DuxCommerce.Catalogue
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open CompositionRoot

[<ApiController>]
[<Route("api/[controller]")>]
type ProductsController (logger : ILogger<ProductsController>) =
    inherit ControllerBase()

    [<HttpGet("{id}")>]
    member this.Get(id : int64) =
        let result = getProduct id
        match result with
        | Ok p -> ActionResult<IActionResult>(base.Ok(p))
        | Error m -> ActionResult<IActionResult>(base.BadRequest(m))
        
    [<HttpPost>]
    member this.Post(request: ProductInfo) =
        let result = createProduct request
        match result with
        | Ok p -> ActionResult<IActionResult>(base.Ok(p))
        | Error m -> ActionResult<IActionResult>(base.BadRequest(m))
        
    [<HttpPut>]
    member this.Put(request: ProductInfo) =
        let result = updateProduct request
        match result with
        | Ok p -> ActionResult<IActionResult>(base.Ok(p))
        | Error m -> ActionResult<IActionResult>(base.BadRequest(m))