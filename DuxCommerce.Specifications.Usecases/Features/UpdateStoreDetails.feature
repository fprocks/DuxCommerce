Feature: Update Store Details
	In order to customize my online store
	As a store admin
	I want to update my store details

Background: 
	Given Tom already created the following store details:
	| StoreName | ContactEmail | SenderEmail | BusinessName | PhoneNumber | TimeZoneId | UnitSystem   | WeightUnit | LengthUnit |
	| Deals365  | c@gmail.com  | s@gmail.com | Deals365 PTY | 89457621    | UTC        | MetricSystem | Gram       | Centimeter |
	And Tom already created the following store address:
	| Address1        | Address2 | Address3 | City   | PostalCode | State           | Country |
	| 1 Market Street |          |          | Sydney | 2000       | New South Wales | AU      |

Scenario: Update store details - green path
	And Tom enters the following store details:
	| StoreName | ContactEmail      | SenderEmail      | BusinessName | PhoneNumber | TimeZoneId | UnitSystem   | WeightUnit   | LengthUnit   |
	| Deals360  | contact@gmail.com | sender@gmail.com | Deals360 PTY | 89457688    | UTC+2      | <UnitSystem> | <WeightUnit> | <LengthUnit> |
	And Tome enters the following store address:
	| Address1 | Address2        | Address3 | City      | PostalCode | State    | Country |
	| Unit 7   | 2 Market Street |          | Melbourne | 3000       | Victoria | AU      |
	When Tom updates the store details
	Then Tom should receive status codes OK
	And the store details should be created as follow:
	| StoreName | ContactEmail      | SenderEmail      | BusinessName | PhoneNumber | TimeZoneId | UnitSystem   | WeightUnit   | LengthUnit   |
	| Deals360  | contact@gmail.com | sender@gmail.com | Deals360 PTY | 89457688    | UTC+2      | <UnitSystem> | <WeightUnit> | <LengthUnit> |
	And the store address should be created as follow:
	| Address1 | Address2        | Address3 | City      | PostalCode | State    | Country |
	| Unit 7   | 2 Market Street |          | Melbourne | 3000       | Victoria | AU      |
Examples: 
	| UnitSystem     | WeightUnit | LengthUnit |
	| MetricSystem   | Kilogram   | Meter      |
	| ImperialSystem | Pound      | Foot       |
	| ImperialSystem | Ounce      | Inch       |

Scenario: Update store details - red path
	And Tom enters the following store details:
	| StoreName | ContactEmail | SenderEmail | BusinessName | PhoneNumber | TimeZoneId | UnitSystem   | WeightUnit   | LengthUnit   |
	| Deals365  | c@gmail.com  | s@gmail.com | Deals365 PTY | 89457621    | UTC        | <UnitSystem> | <WeightUnit> | <LengthUnit> |
	And Tome enters the following store address:
	| Address1        | Address2 | Address3 | City   | PostalCode | State           | Country |
	| 1 Market Street |          |          | Sydney | 2000       | New South Wales | AU      |
	When Tom updates the store details
	Then Tom should receive status codes BadRequest
Examples: 
	| UnitSystem     | WeightUnit | LengthUnit | Comment            |
	| MetricSystem   | Pound      | Centimeter | Invalid WeightUnit |
	| MetricSystem   | Kilogram   | Foot       | Invalid LengthUnit |
	| ImperialSystem | Pound      | Centimeter | Invalid WeightUnit |
	| ImperialSystem | Kilogram   | Inch       | Invalid LengthUnit |