namespace DuxCommerce.Adapters.HttpControllers

open DuxCommerce.Common
open DuxCommerce.Shipping
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open DuxCommerce.Shipping.PublicTypes

[<ApiController>]
[<Route("api/[controller]")>]
type StoreProfileController (logger : ILogger<StoreProfileController>) =
    inherit DuxControllerBase()

    [<HttpGet("{id}")>]
    member this.Get(id : string) : IActionResult=
        let result = ConfigReader.execute (StoreProfileUseCases.getProfile id)
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)
        
    [<HttpPost>]
    member this.Post(request: StoreProfileDto) : IActionResult =
        let result = ConfigReader.execute (StoreProfileUseCases.createProfile request)
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)
        
    [<HttpPut("{id}")>]
    member this.Put(id: string, request: StoreProfileDto) : IActionResult=
        let result = ConfigReader.execute (StoreProfileUseCases.updateProfile id request)
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.Convert(m)
