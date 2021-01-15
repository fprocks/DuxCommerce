namespace DuxCommerce.Payments

open DuxCommerce.Payments.Dto
open DuxCommerce.Payments.MongoRepos
open DuxCommerce.Common
open DuxCommerce.Payments.PublicTypes

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