namespace DuxCommerce.Core.Checkout

open DuxCommerce.Core.Checkout.PublicTypes
open DuxCommerce.Common
open DuxCommerce.Core.Checkout.Commands
open DuxCommerce.Core.Checkout.MongoRepos
open DuxCommerce.Core.Checkout.Dto
open DuxCommerce.Core.Checkout.InternalTypes

module CheckoutUseCases = 

    type UpdateCustomerInfo = string -> CustomerInfoRequest -> ConfigReader<Result<CheckoutDto, CustomError>>
    let updateCustomerInfo :UpdateCustomerInfo = 
        fun shopperId request ->
            readerResult {
                let! dto = CheckoutRepo.getCheckout shopperId

                let! cmd = 
                    CustomerInfoCommand.fromRequest request
                    |> ConfigReader.retn

                let! checkout = 
                    dto
                    |> CheckoutDto.toDomain
                    |> CustomError.mapValidation
                    |> ConfigReader.retn

                // Todo: why the commented code does not work
                //let _ = 
                //    cmd 
                //    |> Checkout.updateCustomerInfo checkout
                //    |> CheckoutDto.fromDomain
                //    |> CheckoutRepo.saveCheckout

                let updatedDto = 
                    cmd 
                    |> Checkout.updateCustomerInfo checkout
                    |> CheckoutDto.fromDomain

                do! CheckoutRepo.saveCheckout updatedDto

                return! CheckoutRepo.getCheckout shopperId
            }