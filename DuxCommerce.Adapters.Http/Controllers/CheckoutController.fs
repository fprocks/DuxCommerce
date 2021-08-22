namespace DuxCommerce.Adapters.Http

open DuxCommerce.Common
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open DuxCommerce.Core.Checkout.PublicTypes
open DuxCommerce.Core.Checkout

[<ApiController>]
[<Route("api/[controller]")>]
type CheckoutController (logger : ILogger<ShoppingCartController>) =
    inherit DuxController()

    // Note: pass in shopperId from front end before we can read it from ShopperContext
    [<HttpPost("{shopperId}/customerinfo")>]
    member this.Post(shopperId: string, request: CustomerInformationRequest) : IActionResult =
        let result = ConfigReader.execute (CheckoutUseCases.addCustomerInfo shopperId request)
        match result with
        | Ok c -> base.Ok(c) :> _
        | Error m -> base.Convert(m)
  