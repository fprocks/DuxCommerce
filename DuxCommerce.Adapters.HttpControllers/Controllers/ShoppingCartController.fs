namespace DuxCommerce.Adapters.HttpControllers

open DuxCommerce.ShoppingCarts
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open CompositionRoot

[<ApiController>]
[<Route("api/[controller]")>]
type ShoppingCartController (logger : ILogger<ShoppingCartController>) =
    inherit ControllerBase()

    [<HttpPost("items")>]
    member this.Post(request: AddCartItemRequest) : IActionResult =
        let result = addCartItem 1000L request
        match result with
        | Ok p -> base.Ok(p) :> _
        | Error m -> base.BadRequest(m) :> _
  