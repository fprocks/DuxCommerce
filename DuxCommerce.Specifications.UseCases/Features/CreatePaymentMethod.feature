Feature: CreatePaymentMethod
	In order to receive payment from my customers
	As a store admin
	I want to create payment method

Scenario: Create payment method
	Given Tom enters the following payment method information:
	| MethodName   | MethodType   | AdditionalDetails   | PaymentInstructions   | PaymentGateway   |
	| <MethodName> | <MethodType> | <AdditionalDetails> | <PaymentInstructions> | <PaymentGateway> |
	When Tome saves the payment method
	Then Tom should receive status codes OK
Examples: 
	| MethodName       | MethodType     | AdditionalDetails | PaymentInstructions | PaymentGateway |
	| Cash on Delivery | CashOnDelivery | Details1          | Instruction1        |                |
	| Bank Deposit     | BankDeposit    | Details2          | Instruction2        |                |
	| Money Order      | MoneyOrder     | Details3          | Instruction3        |                |
	| Credit Card      | CreditCard     |                   |                     | Test Gateway   |
