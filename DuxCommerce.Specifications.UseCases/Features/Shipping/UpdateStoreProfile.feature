Feature: Update Store Profile
	In order to customize my online store
	As a store admin
	I want to update my store profile

Background: 
	Given Tom already created the following store profile:
	| StoreName | ContactEmail | SenderEmail | BusinessName | PhoneNumber | TimeZoneId | UnitSystem   | WeightUnit | LengthUnit |
	| Deals365  | c@gmail.com  | s@gmail.com | Deals365 PTY | 89457621    | UTC        | MetricSystem | Gram       | Centimeter |
	And Tom already created the following store address:
	| FirstName | LastName | AddressLine1    | AddressLine2 | City   | PostalCode | StateName       | CountryCode |
	| James     | Harper   | 1 Market Street |              | Sydney | 2000       | New South Wales | AU          |

Scenario: Update store profile - green path
	And Tom enters the following store profile:
	| StoreName | ContactEmail      | SenderEmail      | BusinessName | PhoneNumber | TimeZoneId | UnitSystem   | WeightUnit   | LengthUnit   |
	| Deals360  | contact@gmail.com | sender@gmail.com | Deals360 PTY | 89457688    | UTC+2      | <UnitSystem> | <WeightUnit> | <LengthUnit> |
	And Tome enters the following store address:
	| FirstName | LastName | AddressLine1 | AddressLine2    | City      | PostalCode | StateName | CountryCode |
	| James     | Harper   | Unit 7       | 2 Market Street | Melbourne | 3000       | Victoria  | AU          |
	When Tom updates the store profile
	Then Tom should receive status codes OK
	And the store profile should be updated as follow:
	| StoreName | ContactEmail      | SenderEmail      | BusinessName | PhoneNumber | TimeZoneId | UnitSystem   | WeightUnit   | LengthUnit   |
	| Deals360  | contact@gmail.com | sender@gmail.com | Deals360 PTY | 89457688    | UTC+2      | <UnitSystem> | <WeightUnit> | <LengthUnit> |
	And the store address should be updated as follow:
	| FirstName | LastName | AddressLine1 | AddressLine2    | City      | PostalCode | StateName | CountryCode |
	| James     | Harper   | Unit 7       | 2 Market Street | Melbourne | 3000       | Victoria  | AU          |
Examples: 
	| UnitSystem     | WeightUnit | LengthUnit |
	| MetricSystem   | Kilogram   | Meter      |
	| MetricSystem   | Gram       | Centimeter |
	| ImperialSystem | Pound      | Foot       |
	| ImperialSystem | Ounce      | Inch       |

Scenario: Update store profile - red path
	And Tom enters the following store profile:
	| StoreName | ContactEmail | SenderEmail | BusinessName | PhoneNumber | TimeZoneId | UnitSystem   | WeightUnit   | LengthUnit   |
	| Deals365  | c@gmail.com  | s@gmail.com | Deals365 PTY | 89457621    | UTC        | <UnitSystem> | <WeightUnit> | <LengthUnit> |
	And Tome enters the following store address:
	| FirstName | LastName | AddressLine1    | AddressLine2 | City   | PostalCode | StateName       | CountryCode |
	| James     | Harper   | 1 Market Street |              | Sydney | 2000       | New South Wales | AU          |
	When Tom updates the store profile
	Then Tom should receive status codes BadRequest
Examples: 
	| UnitSystem     | WeightUnit | LengthUnit | Comment            |
	| MetricSystem   | Pound      | Centimeter | Invalid WeightUnit |
	| MetricSystem   | Kilogram   | Foot       | Invalid LengthUnit |
	| ImperialSystem | Pound      | Centimeter | Invalid WeightUnit |
	| ImperialSystem | Kilogram   | Inch       | Invalid LengthUnit |