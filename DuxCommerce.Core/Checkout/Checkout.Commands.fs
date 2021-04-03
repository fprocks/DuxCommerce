namespace DuxCommerce.Core.Checkout.Commands

open DuxCommerce.Common
open DuxCommerce.Core.Shared.InternalTypes
open DuxCommerce.Core.Checkout.PublicTypes
open DuxCommerce.Core.Shared.Dto

type CustomerInfoCommand = {
    Email : EmailAddress
    ShippingAddress: Address
}

module CustomerInfoCommand =

    let fromRequest (request:CustomerInfoRequest) :Result<CustomerInfoCommand, CustomError> =
        result {
            let! email = 
                EmailAddress.create "" request.Email
                |> CustomError.mapValidation

            let! shippingAddress = 
                request.ShippingAddress
                |> AddressDto.toDomain
                |> CustomError.mapValidation

            return {
                Email = email
                ShippingAddress = shippingAddress
            }
        }