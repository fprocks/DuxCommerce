Feature: Create Tax Rates
	In order to charge taxes from customers
	As a store admin
	I want to add tax rates to my store

Scenario: Create tax rate
	And Tom enters tax rate name GST
	And Tom selects the following countries:
	| CountryCode |
	| AU          |
	| NZ          |
	And Tom selects the following states:
	| CountryCode | Name            |
	| AU          | New South Wales |
	| AU          | Queensland      |
	| AU          | Victoria        |
	| NZ          | Auckland        |
	| NZ          | Wellington      |
	| NZ          | Southland       |	
	And Tom enters the following postal codes:
	| CountryCode | PostalCodes    |
	| AU          | 2000,2001,4000 |
	| NZ          | 2571,2576,2800 |
	And Tom enters tax rate amount <TaxRate>
	When Tom saves the tax rate
	Then Tom should receive status codes OK
	And Tax rate should be created as expected