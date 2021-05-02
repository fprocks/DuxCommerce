Feature: Create Payment Methods
	In order to receive payment from my customers
	As a store admin
	I want to add payment methods to my store

Scenario: Create payment methods - green path
	Given Tom enters the following payment method information:
	| Name   | Type   | AdditionalDetails   | PaymentInstructions   |
	| <Name> | <Type> | <AdditionalDetails> | <PaymentInstructions> |
	When Tom saves the payment method
	Then Tom should receive status codes OK
	And payment method should be created as expected
Examples: 
	| Name             | Type           | AdditionalDetails | PaymentInstructions |
	| Cash on Delivery | CashOnDelivery | Details1          | Instruction1        |
	| Bank Deposit     | BankDeposit    | Details2          | Instruction2        |
	| Money Order      | MoneyOrder     | Details3          | Instruction3        |

Scenario: Create payment methods - red path
	Given Tom enters the following payment method information:
	| Name   | Type   | AdditionalDetails   | PaymentInstructions   |
	| <Name> | <Type> | <AdditionalDetails> | <PaymentInstructions> |
	When Tom saves the payment method
	Then Tom should receive status codes BadRequest
Examples: 
	| Name                                                | Type             | AdditionalDetails | PaymentInstructions | Comment                           |
	| Cash on Delivery                                    | Cash On Delivery | Details1          | Instruction1        | Invalid method type               |
	| Bank Deposit                                        | Bank Deposit     | Details2          | Instruction2        | Invalid method type               |
	| Money Order                                         | Money Order      | Details3          | Instruction3        | Invalid method type               |
	| Payment method name that is more than 50 characters | MoneyOrder       | Details3          | Instruction3        | Method name exceeds 50 characters |