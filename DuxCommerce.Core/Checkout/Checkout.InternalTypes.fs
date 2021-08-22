namespace DuxCommerce.Core.Checkout.InternalTypes

open DuxCommerce.Core.Checkout.SimpleTypes
open DuxCommerce.Core.ShoppingCarts.SimpleTypes
open DuxCommerce.Common
open DuxCommerce.Core.Shared.InternalTypes
open DuxCommerce.Core.Payments.SimpleTypes
open DuxCommerce.Core.Shipping.SimpleTypes
open DuxCommerce.Core.Checkout.Commands

type Checkout = {
    CheckoutId : CheckoutId
    ShopperId : ShopperId
    Email : EmailAddress
    ShippingAddress : Address
    SameAsBilling : bool
    BillingAddress : Address
    ShippingMethodId : ShippingMethodId
    PaymentMethodId : PaymentMethodId
}

module Checkout = 
    let addCustomerInfo checkout (cmd:CustomerInformationCommand) :Checkout=
        {checkout with Email = cmd.Email; ShippingAddress = cmd.ShippingAddress }