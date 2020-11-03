namespace DuxCommerce.Adapters.HttpControllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Compositions
open DuxCommerce.Catalogue.PublicTypes

[<ApiController>]
[<Route("api/[controller]")>]
type ProductsController (logger : ILogger<ProductsController>) =
    inherit DuxControllerBase()

    [<HttpGet("{id}")>]
    member this.Get(id : int64) : IActionResult=
        let result = getProduct' id
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)
        
    [<HttpPost>]
    member this.Post(request: ProductInfo) : IActionResult =
        let result = createProduct' request
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)
        
    [<HttpPut("{id}")>]
    member this.Put(id: int64, request: ProductInfo) : IActionResult=
        let result = updateProduct' id request
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)
