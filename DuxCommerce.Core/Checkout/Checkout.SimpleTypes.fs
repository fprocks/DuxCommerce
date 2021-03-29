namespace DuxCommerce.Core.Checkout.SimpleTypes

type CheckoutId = private CheckoutId of string

module CheckoutId =

    let value (CheckoutId id) = id

    let create id = CheckoutId id