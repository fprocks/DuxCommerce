namespace DuxCommerce.Adapters.HttpControllers

open DuxCommerce.Common
open DuxCommerce.ShoppingCarts
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open DuxCommerce.ShoppingCarts.PublicTypes

[<ApiController>]
[<Route("api/[controller]")>]
type ShoppingCartController (logger : ILogger<ShoppingCartController>) =
    inherit DuxControllerBase()

    // Note: pass in shopperId from front end before we can read it from ShopperContext
    [<HttpPost("{shopperId}/items")>]
    member this.Post(shopperId: int64, request: AddCartItemRequest) : IActionResult =
        let result = ConfigReader.execute (UseCases.addCartItem shopperId request)
        match result with
        | Ok c -> base.Ok(c) :> _
        | Error m -> base.Convert(m)

    [<HttpPut("{shopperId}")>]
    member this.Put(shopperId: int64, request: UpdateCartRequest) : IActionResult =
        let result = ConfigReader.execute (UseCases.updateCart shopperId request)
        match result with
        | Ok c -> base.Ok(c) :> _
        | Error m -> base.Convert(m)

    [<HttpDelete("{shopperId}/items")>]
    member this.Delete(shopperId: int64, request: DeleteCartItemRequest) : IActionResult =
        let result = ConfigReader.execute (UseCases.deleteCartItem shopperId request)
        match result with
        | Ok c -> base.Ok(c) :> _
        | Error m -> base.Convert(m)
  