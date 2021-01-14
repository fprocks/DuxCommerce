namespace DuxCommerce.Payment

open DuxCommerce.Payment.Dto
open DuxCommerce.Payment.MongoRepos
open DuxCommerce.Common
open DuxCommerce.Payment.PublicTypes

module PaymentMethodUseCases =

    type CreatePaymentMethodUseCase = PaymentMethodDto -> ConfigReader<Result<PaymentMethodDto, CustomError>>
    let createMethod :CreatePaymentMethodUseCase = 
        fun methodDto ->
            readerResult {
                let! _ = methodDto
                        |> PaymentMethodDto.toDomain 
                        |> ConfigReader.retn

                let! methodId = methodDto |> PaymentMethodRepo.createMethod
                return! PaymentMethodRepo.getMethod methodId
            }