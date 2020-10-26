namespace DuxCommerce.Adapters.HttpControllers

open DuxCommerce.ShoppingCarts
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open ShoppingCartComposition

[<ApiController>]
[<Route("api/[controller]")>]
type ShoppingCartController (logger : ILogger<ShoppingCartController>) =
    inherit ControllerBase()

    [<HttpPost("items")>]
    member this.Post(request: AddCartItemRequest) : IActionResult =
        let result = addCartItem request
        match result with
        | Ok c -> base.Ok(c) :> _
        | Error m -> base.BadRequest(m) :> _
  