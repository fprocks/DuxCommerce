Feature: Create Tax Rates
	In order to charge taxes from customers
	As a store admin
	I want to add tax rates to my store

Scenario: Create tax rates
	Given Tom selects country <CountryCode>
	And Tome selects state <StateName>
	And Tom enters tax rate <TaxRate>
	When Tom saves the tax rate
	Then Tom should receive status codes OK
	And Tax rate should be created as expected
Examples: 
	| CountryCode | StateName       | TaxRate |
	| AU          | New South Wales | 10      |
	| AU          | Queensland      | 20      |
	| NZ          | Auckland        | 30      |
	| NZ          | Wellington      | 40      |