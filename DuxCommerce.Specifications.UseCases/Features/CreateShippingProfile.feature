Feature: Create Shipping Profile
	In order to customize my online store
	As a store admin
	I want to create my shipping profile

Background: 
	Given Tom already created the following shipping origins:
	| OriginId | FirstName | LastName | AddressLine1    | AddressLine2 | City   | PostalCode | StateName       | CountryCode |
	| 1        | James     | Harper   | 1 Market Street |              | Sydney | 2000       | New South Wales | AU          |

Scenario: Create custom shipping profile
	And Tom enters shipping profile name Heavy Products
	And Tom selects shipping origin 1
	And Tom selects the following shipping countries:
	| CountryCode |
	| AU          |
	| NZ          |
	And Tom selects the following shipping states:
	| CountryCode | Name            |
	| AU          | New South Wales |
	| AU          | Queensland      |
	| AU          | Victoria        |
	| NZ          | Auckland        |
	| NZ          | Wellington      |
	And Tom selects the rate type <RateType>
	And Tom enters the rate name <RateName>
	And Tome enters the following rates:
	| Min | Max   | Rate |
	| 0   | 100   | 50   |
	| 100 | 200   | 100  |
	| 200 | 40000 | 200  |
	When Tom saves the shipping profile
	Then Tom should receive status codes OK
	And shipping profile should be saved as expected
Examples: 
	| RateType     | RateName       |
	| ByWeight     | By Weight      |
	| ByQuantity   | By Quantity    |
	| ByOrderTotal | By Order Total |
	| FixedRate    | Fixed Rate     |