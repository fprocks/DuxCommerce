namespace DuxCommerce.Core.Payments.Dto

open DuxCommerce.Common
open DuxCommerce.Core.Payments.InternalTypes
open DuxCommerce.Core.Payments.SimpleTypes
open DuxCommerce.Core.Payments.PublicTypes

module PaymentMethodDto = 

    let toDomain (methodDto:PaymentMethodDto) :Result<PaymentMethod, CustomError> = 
        result {
            let! name = String50.create "MethodName" methodDto.Name
            let! methodType = PaymentMethodType.create methodDto.Type
            let! details = String255.create "AdditionalDetails" methodDto.AdditionalDetails
            let! instructions = String255.create "PaymentInstructions" methodDto.PaymentInstructions

            return {
                PaymentMethodId = PaymentMethodId methodDto.Id
                Name = name
                Type = methodType
                AdditionalDetails = details
                PaymentInstructions = instructions
            }
        } |> CustomError.mapValidation

    let fromDomain (payment:PaymentMethod) :PaymentMethodDto = 
        {
            Id = PaymentMethodId.value payment.PaymentMethodId
            Name = String50.value payment.Name
            Type = PaymentMethodType.value payment.Type
            AdditionalDetails = String255.value payment.AdditionalDetails
            PaymentInstructions = String255.value payment.PaymentInstructions
        }