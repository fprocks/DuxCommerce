Feature: CreatePaymentMethod
	In order to receive payment from my customers
	As a store admin
	I want to create payment method

Scenario: Create manual payment methods
	Given Tom enters the following payment method information:
	| Name   | Type   | AdditionalDetails   | PaymentInstructions   |
	| <Name> | <Type> | <AdditionalDetails> | <PaymentInstructions> |
	When Tome saves the payment method
	Then Tom should receive status codes OK
	And payment method should be created as expected
Examples: 
	| Name             | Type           | AdditionalDetails | PaymentInstructions |
	| Cash on Delivery | CashOnDelivery | Details1          | Instruction1        |
	| Bank Deposit     | BankDeposit    | Details2          | Instruction2        |
	| Money Order      | MoneyOrder     | Details3          | Instruction3        |