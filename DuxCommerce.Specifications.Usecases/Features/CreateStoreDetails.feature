Feature: Create Store Details
	In order to customize my online store
	As a store admin
	I want to save my store details

Scenario: Create store details - green path
	Given Tom enters the following store details:
	| StoreName | ContactEmail | SenderEmail | BusinessName | PhoneNumber | TimeZoneId | UnitSystem   | WeightUnit   | LengthUnit   |
	| Deals365  | c@gmail.com  | s@gmail.com | Deals365 PTY | 89457621    | UTC        | <UnitSystem> | <WeightUnit> | <LengthUnit> |
	And Tome enters the following store address:
	| Address1        | Address2 | Address3 | City   | PostalCode | State           | Country |
	| 1 Market Street |          |          | Sydney | 2000       | New South Wales | AU      |
	When Tom saves the store details
	Then Tom should receive status codes OK
	And the store details should be created as follow:
	| StoreName | ContactEmail | SenderEmail | BusinessName | PhoneNumber | TimeZoneId | UnitSystem   | WeightUnit   | LengthUnit   |
	| Deals365  | c@gmail.com  | s@gmail.com | Deals365 PTY | 89457621    | UTC        | <UnitSystem> | <WeightUnit> | <LengthUnit> |
	And the store address should be created as follow:
	| Address1        | Address2 | Address3 | City   | PostalCode | State           | Country |
	| 1 Market Street |          |          | Sydney | 2000       | New South Wales | AU      |
Examples: 
	| UnitSystem     | WeightUnit | LengthUnit |
	| MetricSystem   | Gram       | Centimeter |
	| MetricSystem   | Kilogram   | Meter      |
	| ImperialSystem | Pound      | Foot       |
	| ImperialSystem | Ounce      | Inch       |

Scenario: Create store details - red path
	Given Tom enters the following store details:
	| StoreName | ContactEmail | SenderEmail | BusinessName | PhoneNumber | TimeZoneId | UnitSystem   | WeightUnit   | LengthUnit   |
	| Deals365  | c@gmail.com  | s@gmail.com | Deals365 PTY | 89457621    | UTC        | <UnitSystem> | <WeightUnit> | <LengthUnit> |
	And Tome enters the following store address:
	| Address1        | Address2 | Address3 | City   | PostalCode | State           | Country |
	| 1 Market Street |          |          | Sydney | 2000       | New South Wales | AU      |
	When Tom saves the store details
	Then Tom should receive status codes BadRequest
Examples: 
	| UnitSystem     | WeightUnit | LengthUnit | Comment            |
	| MetricSystem   | Pound      | Centimeter | Invalid WeightUnit |
	| MetricSystem   | Kilogram   | Foot       | Invalid LengthUnit |
	| ImperialSystem | Pound      | Centimeter | Invalid WeightUnit |
	| ImperialSystem | Kilogram   | Inch       | Invalid LengthUnit |