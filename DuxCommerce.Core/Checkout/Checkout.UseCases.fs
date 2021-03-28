namespace DuxCommerce.Core.Checkout

open DuxCommerce.Core.Checkout.PublicTypes
open DuxCommerce.Common
open DuxCommerce.Core.Shared.Dto

module CheckoutUseCases = 

    type AddShippingAddress = string -> CheckoutDto -> ConfigReader<Result<CheckoutDto, CustomError>>
    let addShippingAddress :AddShippingAddress = 
        fun shopperId checkoutDto ->
            readerResult {
                let! _ = EmailAddress.create "" checkoutDto.Email 
                            |> CustomError.mapValidation
                            |> ConfigReader.retn
                let! _ = checkoutDto.ShippingAddress 
                                |> AddressDto.toDomain 
                                |> CustomError.mapValidation
                                |> ConfigReader.retn
                return checkoutDto
            }