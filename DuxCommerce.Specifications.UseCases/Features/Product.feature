Feature: Add a product
	In order to sell products online
	As a store admin
	I want to add proucts to my store first

Scenario: Add a product - green path
	Given Tom enters the following product information:
	| Index | Name | Description     | Price | Retail | Cost | Length | Width | Height | Weight | ShippingType    | Sku      | Barcode       | TrackInventory | OutOfStockRule  |
	| 1     | DDD  | DDD Description | 100   | 120    | 80   | 1      | 2     | 3      | 4      | ShipSeparately  | ddd-book | 1234567890111 | True           | Remove          |
	| 2     | BDD  | BDD Description | 90    | 110    | 70   | 2      | 3     | 4      | 5      | PhysicalProduct | bdd-book | 1234567890222 | False          | ContinueSelling |
	| 3     | TDD  | TDD Description | 80    | 100    | 60   | 3      | 4     | 5      | 6      | DigitalProduct  | tdd-book | 1234567890333 | True           | StopSelling     |
	When Tom saves the products
	Then Tom should receive success result
	And the products should be created as follow:
	| Index | Name | Description     | Price | Retail | Cost | Length | Width | Height | Weight | ShippingType    | Sku      | Barcode       | TrackInventory | OutOfStockRule  |
	| 1     | DDD  | DDD Description | 100   | 120    | 80   | 1      | 2     | 3      | 4      | ShipSeparately  | ddd-book | 1234567890111 | Yes            | Remove          |
	| 2     | BDD  | BDD Description | 90    | 110    | 70   | 2      | 3     | 4      | 5      | PhysicalProduct | bdd-book | 1234567890222 | No             | ContinueSelling |
	| 3     | TDD  | TDD Description | 80    | 100    | 60   | 3      | 4     | 5      | 6      | DigitalProduct  | tdd-book | 1234567890333 | Yes            | StopSelling     |

Scenario: Add a product - red path
	Given Tom enters the following product information:
	| Index | Name | Description     | Price | Retail | Cost | Length | Width | Height | Weight | ShippingType    | Sku      | Barcode       | TrackInventory | OutOfStockRule   | Comment                 |
	| 1     | DDD  | DDD Description | 100   | 120    | 80   | 1      | 2     | 3      | 4      | Ship Separately | ddd-book | 1234567890111 | Yes            | Remove           | Invalid ShippingType    |
	| 2     | BDD  | BDD Description | 90    | 110    | 70   | 2      | 3     | 4      | 5      | PhysicalProduct | bdd-book | 1234567890222 | No             | Continue Selling | Invalid OutOfStock Rule |
	When Tom saves the products
	Then Tom should receive failure result