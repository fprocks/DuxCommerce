Feature: Create Shipping Profile
	In order to customize my online store
	As a store admin
	I want to save my store profile

Scenario: Create default shipping profile
	Given Tom enters the following store profile:
	| StoreName | ContactEmail | SenderEmail | BusinessName | PhoneNumber | TimeZoneId | UnitSystem   | WeightUnit | LengthUnit |
	| Deals365  | c@gmail.com  | s@gmail.com | Deals365 PTY | 89457621    | UTC        | MetricSystem | Gram       | Centimeter |
	And Tome enters the following store address:
	| AddressLine1    | AddressLine2 | City   | PostalCode | StateName       | CountryCode |
	| 1 Market Street |              | Sydney | 2000       | New South Wales | AU          |
	When Tom saves the store profile
	Then Tom should receive status codes OK
	And default shipping profile should be created as follow:
	| Name            | IsDefault |
	| Default Profile | True      |
	And shipping origin should be created as follow:
	| Name            | AddressLine1    | AddressLine2 | City   | PostalCode | StateName       | CountryCode | IsDefault |
	| 1 Market Street | 1 Market Street |              | Sydney | 2000       | New South Wales | AU          | True      |
	And shipping zone should be created as follow:
	| Name         |
	| AU |
	And shippig countries should be created as follow:
	| CountryCode |
	| AU          |
	And shippig states should be created as follow:
	| CountryCode | Name                         |
	| AU          | Australian Capital Territory |
	| AU          | New South Wales              |
	| AU          | Northern Territory           |
	| AU          | Queensland                   |
	| AU          | South Australia              |
	| AU          | Tasmania                     |
	| AU          | Victoria                     |
	| AU          | Western Australia            |