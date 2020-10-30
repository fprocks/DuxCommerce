namespace DuxCommerce.Adapters.HttpControllers

open DuxCommerce.ShoppingCarts
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open ShoppingCartComposition

[<ApiController>]
[<Route("api/[controller]")>]
type ShoppingCartController (logger : ILogger<ShoppingCartController>) =
    inherit ControllerBase()

    // Todo: pass in shopperId from front end before we can read it from ShopperContext
    [<HttpPost("{shopperId}/items")>]
    member this.Post(shopperId: int64, request: AddCartItemRequest) : IActionResult =
        let result = addCartItem shopperId request
        match result with
        | Ok c -> base.Ok(c) :> _
        | Error m -> base.BadRequest(m) :> _

    [<HttpPut("{shopperId}")>]
    member this.Put(shopperId: int64, request: UpdateCartRequest) : IActionResult =
        let result = updateCart shopperId request
        match result with
        | Ok c -> base.Ok(c) :> _
        | Error m -> base.BadRequest(m) :> _

    [<HttpDelete("{shopperId}/items")>]
    member this.Delete(shopperId: int64, request: DeleteCartItemRequest) : IActionResult =
        let result = deleteCartItem shopperId request
        match result with
        | Ok c -> base.Ok(c) :> _
        | Error m -> base.BadRequest(m) :> _
  