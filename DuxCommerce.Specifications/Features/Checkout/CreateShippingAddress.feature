Feature: ShippingAddress
	Simple calculator for adding two numbers

Background: 
	Given the following products are already created:
	| Index | Name | Description     | Price | Retail | Cost | Length | Width | Height | Weight | ProductType     | Sku      | Barcode       | TrackInventory | OutOfStockRule  |
	| 1     | DDD  | DDD Description | 100   | 120    | 80   | 1      | 2     | 3      | 4      | DigitalProduct  | ddd-book | 1234567890111 | True           | Remove          |
	| 2     | BDD  | BDD Description | 50    | 110    | 70   | 2      | 3     | 4      | 5      | PhysicalProduct | bdd-book | 1234567890222 | False          | ContinueSelling |
	| 3     | TDD  | TDD Description | 80    | 100    | 60   | 3      | 4     | 5      | 6      | DigitalProduct  | tdd-book | 1234567890333 | True           | StopSelling     |

Scenario: Add two numbers
	And Amy adds the following products to her shopping cart:
	| Product | Name | Quantity |
	| 1       | DDD  | 4        |
	| 2       | BDD  | 8        |
	When the two numbers are added
	Then the result should be 120