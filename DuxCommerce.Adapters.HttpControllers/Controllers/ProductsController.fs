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
        let result = getProductUseCase id
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)
        
    [<HttpPost>]
    member this.Post(request: ProductDto) : IActionResult =
        let result = createProductUseCase request
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)
        
    [<HttpPut("{id}")>]
    member this.Put(id: int64, request: ProductDto) : IActionResult=
        let result = updateProductUseCase id request
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)
