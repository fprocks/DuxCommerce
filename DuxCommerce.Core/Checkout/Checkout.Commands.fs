namespace DuxCommerce.Core.Checkout.Commands

open DuxCommerce.Common
open DuxCommerce.Core.Shared.InternalTypes
open DuxCommerce.Core.Checkout.PublicTypes
open DuxCommerce.Core.Shared.Dto

type CustomerInformationCommand = {
    Email : EmailAddress
    ShippingAddress: Address
    }

module CustomerInformationCommand =

    let fromRequest (request:CustomerInformationRequest) :Result<CustomerInformationCommand, CustomError> =
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