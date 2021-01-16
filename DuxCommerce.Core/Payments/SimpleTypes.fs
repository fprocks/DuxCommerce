namespace DuxCommerce.Core.Payments.SimpleTypes

open DuxCommerce.Core.Shared

type PaymentMethodId = PaymentMethodId of string
module PaymentMethodId =
    let value (PaymentMethodId id) = id    
    let create id = PaymentMethodId id