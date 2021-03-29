namespace DuxCommerce.Core.Checkout.Dto

open DuxCommerce.Core.Checkout.PublicTypes
open DuxCommerce.Core.Checkout.InternalTypes
open DuxCommerce.Core.Checkout.SimpleTypes
open DuxCommerce.Core.Shared.Dto
open DuxCommerce.Core.ShoppingCarts.SimpleTypes
open DuxCommerce.Core.Shipping.SimpleTypes
open DuxCommerce.Core.Payments.SimpleTypes
open DuxCommerce.Common

module CheckoutDto =

    let create :CheckoutDto = {
        Id = ""
        ShopperId = ""
        Email = "test@test.com"
        ShippingAddress = None
        SameAsBilling = false
        BillingAddress = None
        ShippingMethodId = ""
        PaymentMethodId = ""
    }

    let fromDomain (checkout:Checkout) :CheckoutDto = 
        {
            Id = CheckoutId.value checkout.CheckoutId
            ShopperId = ShopperId.value checkout.ShopperId
            Email = EmailAddress.value checkout.Email
            ShippingAddress = Option.map AddressDto.fromDomain checkout.ShippingAddress
            SameAsBilling = checkout.SameAsBilling
            BillingAddress = Option.map AddressDto.fromDomain checkout.BillingAddress
            ShippingMethodId = ShippingMethodId.value checkout.ShippingMethodId
            PaymentMethodId = PaymentMethodId.value checkout.PaymentMethodId
        }

    let toDomain (dto:CheckoutDto) :Result<Checkout, string> =
        result {
            let checkoutId = CheckoutId.create dto.Id
            let shopperId = ShopperId.create dto.ShopperId
            let! email = EmailAddress.create "EmailAddress" dto.Email
            let! shippingAddress = Result.bindOption AddressDto.toDomain dto.ShippingAddress
            let! billingAddress = Result.bindOption AddressDto.toDomain dto.BillingAddress
            let shippingMethodId = ShippingMethodId.create dto.ShippingMethodId
            let paymentMethodId = PaymentMethodId.create dto.PaymentMethodId

            return {
                CheckoutId = checkoutId
                ShopperId = shopperId
                Email = email
                ShippingAddress = shippingAddress
                SameAsBilling = dto.SameAsBilling
                BillingAddress = billingAddress
                ShippingMethodId = shippingMethodId
                PaymentMethodId = paymentMethodId
            }
        }