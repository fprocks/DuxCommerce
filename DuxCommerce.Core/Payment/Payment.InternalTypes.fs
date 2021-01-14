namespace DuxCommerce.Payment.InternalTypes

open DuxCommerce.Common
open DuxCommerce.Payment.SimpleTypes

type PaymentMethodType =
    | CashOnDelivery
    | BankDeposit
    | MoneyOrder

module PaymentMethodType =

    let value methodType =
        match methodType with
        | CashOnDelivery -> "CashOnDelivery"
        | BankDeposit -> "BankDeposit"
        | MoneyOrder -> "MoneyOrder"

    let create methodType =
        match methodType with
        | "CashOnDelivery" -> Ok CashOnDelivery
        | "BankDeposit" -> Ok BankDeposit
        | "MoneyOrder" -> Ok MoneyOrder
        | _ -> 
            let msg = "PaymentMethodType must be one of 'CashOnDelivery', 'BankDeposit', 'MoneyOrder'" 
            Error msg

type PaymentMethod = {
    PaymentMethodId : PaymentMethodId
    Name : String50
    Type : PaymentMethodType
    AdditionalDetails: String255
    PaymentInstructions: String255
}