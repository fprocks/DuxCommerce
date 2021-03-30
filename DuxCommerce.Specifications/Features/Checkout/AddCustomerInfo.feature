Feature: Add Customer Information
	In order to get my products delivered
	As an online shopper
	I want to enter my contact details and shipping address during checkout

Background: 
	Given the following products are already created:
	| Index | Name | Description     | Price | Retail | Cost | Length | Width | Height | Weight | ProductType     | Sku      | Barcode       | TrackInventory | OutOfStockRule  |
	| 1     | DDD  | DDD Description | 100   | 120    | 80   | 1      | 2     | 3      | 4      | DigitalProduct  | ddd-book | 1234567890111 | True           | Remove          |
	| 2     | BDD  | BDD Description | 50    | 110    | 70   | 2      | 3     | 4      | 5      | PhysicalProduct | bdd-book | 1234567890222 | False          | ContinueSelling |
	| 3     | TDD  | TDD Description | 80    | 100    | 60   | 3      | 4     | 5      | 6      | DigitalProduct  | tdd-book | 1234567890333 | True           | StopSelling     |

Scenario: Add Customer Information
	And Amy adds the following products to her shopping cart:
	| Product | Name | Quantity |
	| 1       | DDD  | 4        |
	| 2       | BDD  | 8        |
	And Amy enters the email address amy@gmail.com
	And Amy enters the following shipping address
	| FirstName | LastName | AddressLine1 | AddressLine2    | City      | PostalCode | StateName | CountryCode |
	| James     | Harper   | Unit 7       | 2 Market Street | Melbourne | 3000       | Victoria  | AU          |
	When Amy saves her contact details and shipping address
	Then Amy should receive status codes OK
	Then Amy's information should be saved as expected