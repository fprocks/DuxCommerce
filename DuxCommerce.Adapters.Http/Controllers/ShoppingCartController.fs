namespace DuxCommerce.Adapters.Http

open DuxCommerce.Common
open DuxCommerce.Core.ShoppingCarts
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open DuxCommerce.Core.ShoppingCarts.PublicTypes

[<ApiController>]
[<Route("api/[controller]")>]
type ShoppingCartController(logger: ILogger<ShoppingCartController>) =
    inherit DuxController()

    // Note: pass in shopperId from front end before we can read it from ShopperContext
    [<HttpPost("{shopperId}/items")>]
    member this.Post(shopperId: string, request: AddCartItemRequest) : IActionResult =
        let result =
            ConfigReader.execute (ShoppingCartUseCases.addCartItem shopperId request)

        match result with
        | Ok c -> base.Ok(c) :> _
        | Error m -> base.Convert(m)

    [<HttpPut("{shopperId}")>]
    member this.Put(shopperId: string, request: UpdateCartRequest) : IActionResult =
        let result =
            ConfigReader.execute (ShoppingCartUseCases.updateCart shopperId request)

        match result with
        | Ok c -> base.Ok(c) :> _
        | Error m -> base.Convert(m)

    [<HttpDelete("{shopperId}/items")>]
    member this.Delete(shopperId: string, request: DeleteCartItemRequest) : IActionResult =
        let result =
            ConfigReader.execute (ShoppingCartUseCases.deleteCartItem shopperId request)

        match result with
        | Ok c -> base.Ok(c) :> _
        | Error m -> base.Convert(m)
