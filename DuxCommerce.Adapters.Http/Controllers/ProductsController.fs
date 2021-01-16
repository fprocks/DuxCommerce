namespace DuxCommerce.Adapters.Http

open DuxCommerce.Core.Catalogue
open DuxCommerce.Common
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open DuxCommerce.Core.Catalogue.PublicTypes

[<ApiController>]
[<Route("api/[controller]")>]
type ProductsController (logger : ILogger<ProductsController>) =
    inherit DuxControllerBase()

    [<HttpGet("{id}")>]
    member this.Get(id : string) : IActionResult=
        let result = ConfigReader.execute (ProductUseCases.getProduct id)
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)
        
    [<HttpPost>]
    member this.Post(request: ProductDto) : IActionResult =
        let result = ConfigReader.execute (ProductUseCases.createProduct request)
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)
        
    [<HttpPut("{id}")>]
    member this.Put(id: string, request: ProductDto) : IActionResult=
        let result = ConfigReader.execute (ProductUseCases.updateProduct id request)
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)
