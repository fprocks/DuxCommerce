Feature: Create Store Profile
	In order to customize my online store
	As a store admin
	I want to save my store profile

Scenario: Create store profile and address
	Given Tom enters the following store profile:
	| StoreName | ContactEmail | SenderEmail | BusinessName | PhoneNumber | TimeZoneId | UnitSystem   | WeightUnit   | LengthUnit   |
	| Deals365  | c@gmail.com  | s@gmail.com | Deals365 PTY | 89457621    | UTC        | <UnitSystem> | <WeightUnit> | <LengthUnit> |
	And Tome enters the following store address:
	| AddressLine1    | AddressLine2 | City   | PostalCode | StateName       | CountryCode |
	| 1 Market Street |              | Sydney | 2000       | New South Wales | AU          |
	When Tom saves the store profile
	Then Tom should receive status codes OK
	And the store profile should be created as follow:
	| StoreName | ContactEmail | SenderEmail | BusinessName | PhoneNumber | TimeZoneId | UnitSystem   | WeightUnit   | LengthUnit   |
	| Deals365  | c@gmail.com  | s@gmail.com | Deals365 PTY | 89457621    | UTC        | <UnitSystem> | <WeightUnit> | <LengthUnit> |
	And the store address should be created as follow:
	| AddressLine1    | AddressLine2 | City   | PostalCode | StateName       | CountryCode |
	| 1 Market Street |              | Sydney | 2000       | New South Wales | AU          |
Examples: 
	| UnitSystem     | WeightUnit | LengthUnit |
	| MetricSystem   | Gram       | Centimeter |
	| MetricSystem   | Kilogram   | Meter      |
	| ImperialSystem | Pound      | Foot       |
	| ImperialSystem | Ounce      | Inch       |

Scenario: Create store profile - red path
	Given Tom enters the following store profile:
	| StoreName | ContactEmail | SenderEmail | BusinessName | PhoneNumber | TimeZoneId | UnitSystem   | WeightUnit   | LengthUnit   |
	| Deals365  | c@gmail.com  | s@gmail.com | Deals365 PTY | 89457621    | UTC        | <UnitSystem> | <WeightUnit> | <LengthUnit> |
	And Tome enters the following store address:
	| AddressLine1    | AddressLine2 | City   | PostalCode | StateName       | CountryCode |
	| 1 Market Street |              | Sydney | 2000       | New South Wales | AU          |
	When Tom saves the store profile
	Then Tom should receive status codes BadRequest
Examples: 
	| UnitSystem     | WeightUnit | LengthUnit | Comment            |
	| MetricSystem   | Pound      | Centimeter | Invalid WeightUnit |
	| MetricSystem   | Kilogram   | Foot       | Invalid LengthUnit |
	| ImperialSystem | Pound      | Centimeter | Invalid WeightUnit |
	| ImperialSystem | Kilogram   | Inch       | Invalid LengthUnit |

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
	And shipping zones should be created as follow:
	| Name |
	| AU   |
	And shippig countries should be created as follow:
	| CountryCode |
	| AU          |
	And shippig states should be created as follow:
	| StateId | CountryCode | Name                         |
	| 100     | AU          | Australian Capital Territory |
	| 101     | AU          | New South Wales              |
	| 102     | AU          | Northern Territory           |
	| 103     | AU          | Queensland                   |
	| 104     | AU          | South Australia              |
	| 105     | AU          | Tasmania                     |
	| 106     | AU          | Victoria                     |
	| 107     | AU          | Western Australia            |