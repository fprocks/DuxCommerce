namespace DuxCommerce.Adapters.HttpControllers

open DuxCommerce.Catalogue
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open CatalogueComposition
open DuxCommerce.Catalogue.SimpleTypes
open DuxCommerce.Catalogue.PublicTypes

[<ApiController>]
[<Route("api/[controller]")>]
type ProductsController (logger : ILogger<ProductsController>) =
    inherit ControllerBase()

    [<HttpGet("{id}")>]
    member this.Get(id : int64) : IActionResult=
        let result = getProduct' id
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.BadRequest(m) :> _
        
    [<HttpPost>]
    member this.Post(request: ProductInfo) : IActionResult =
        let result = createProduct' request
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.BadRequest(m) :> _
        
    [<HttpPut("{id}")>]
    member this.Put(id: int64, request: ProductInfo) : IActionResult=
        let result = updateProduct' id request
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.BadRequest(m) :> _