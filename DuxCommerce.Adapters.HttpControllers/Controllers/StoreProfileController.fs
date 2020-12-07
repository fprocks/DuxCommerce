namespace DuxCommerce.Adapters.HttpControllers

open DuxCommerce.Common
open DuxCommerce.Settings
open DuxCommerce.Settings.PublicTypes
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging

[<ApiController>]
[<Route("api/[controller]")>]
type StoreProfileController (logger : ILogger<StoreProfileController>) =
    inherit DuxControllerBase()

    [<HttpGet("{id}")>]
    member this.Get(id : int64) : IActionResult=
        let result = ConfigReader.execute (UseCases.getStoreProfile id)
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)
        
    [<HttpPost>]
    member this.Post(request: StoreProfileDto) : IActionResult =
        let result = ConfigReader.execute (UseCases.createStoreProfile request)
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)
        
    [<HttpPut("{id}")>]
    member this.Put(id: int64, request: StoreProfileDto) : IActionResult=
        let result = ConfigReader.execute (UseCases.updateStoreProfile id request)
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)
