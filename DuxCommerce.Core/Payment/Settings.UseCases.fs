namespace DuxCommerce.Payment

open DuxCommerce.Payment.Dto
open DuxCommerce.Payment.MongoRepos
open DuxCommerce.Common
open DuxCommerce.Payment.PublicTypes

type CreatePaymentMethodUseCase = PaymentMethodDto -> ConfigReader<Result<PaymentMethodDto, CustomError>>

module PaymentMethodUseCases =

    let createMethod :CreatePaymentMethodUseCase = 
        fun methodDto ->
            readerResult {
                let! methodId = methodDto |> PaymentMethodRepo.createMethod
                return! PaymentMethodRepo.getMethod methodId
            }