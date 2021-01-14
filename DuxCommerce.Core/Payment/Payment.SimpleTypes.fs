namespace DuxCommerce.Payment.SimpleTypes

open DuxCommerce.Common

type PaymentMethodId = PaymentMethodId of string
module PaymentMethodId =
    let value (PaymentMethodId id) = id    
    let create id = PaymentMethodId id