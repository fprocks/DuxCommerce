namespace DuxCommerce.Core.Payments

open DuxCommerce.Core.Payments.Dto
open DuxCommerce.Core.Payments.MongoRepos
open DuxCommerce.Common
open DuxCommerce.Core.Payments.PublicTypes

module PaymentMethodUseCases =

    type CreatePaymentMethodUseCase = PaymentMethodDto -> ConfigReader<Result<PaymentMethodDto, CustomError>>
    let createMethod :CreatePaymentMethodUseCase = 
        fun methodDto ->
            readerResult {
                let! _ = 
                    methodDto
                    |> PaymentMethodDto.toDomain 
                    |> CustomError.mapValidation
                    |> ConfigReader.retn

                let! methodId = 
                    methodDto 
                    |> PaymentMethodRepo.createMethod

                return! PaymentMethodRepo.getMethod methodId
            }