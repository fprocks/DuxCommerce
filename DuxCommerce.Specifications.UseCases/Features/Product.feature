Feature: Add a product
	In order to sell products online
	As a store admin
	I want to add proucts to my store first

Scenario: Add products - green path
	Given Tom enters the following product information:
	| Index | Name | Description     | Price | Retail | Cost | Length | Width | Height | Weight | ShippingType    | Sku      | Barcode       | TrackInventory | OutOfStockRule  |
	| 1     | DDD  | DDD Description | 100   | 120    | 80   | 1      | 2     | 3      | 4      | ShipSeparately  | ddd-book | 1234567890111 | True           | Remove          |
	| 2     | BDD  | BDD Description | 90    | 110    | 70   | 2      | 3     | 4      | 5      | PhysicalProduct | bdd-book | 1234567890222 | False          | ContinueSelling |
	| 3     | TDD  | TDD Description | 80    | 100    | 60   | 3      | 4     | 5      | 6      | DigitalProduct  | tdd-book | 1234567890333 | True           | StopSelling     |
	When Tom saves the products
	Then Tom should receive status codes OK
	And the products should be created as follow:
	| Index | Name | Description     | Price | Retail | Cost | Length | Width | Height | Weight | ShippingType    | Sku      | Barcode       | TrackInventory | OutOfStockRule  |
	| 1     | DDD  | DDD Description | 100   | 120    | 80   | 1      | 2     | 3      | 4      | ShipSeparately  | ddd-book | 1234567890111 | True           | Remove          |
	| 2     | BDD  | BDD Description | 90    | 110    | 70   | 2      | 3     | 4      | 5      | PhysicalProduct | bdd-book | 1234567890222 | False          | ContinueSelling |
	| 3     | TDD  | TDD Description | 80    | 100    | 60   | 3      | 4     | 5      | 6      | DigitalProduct  | tdd-book | 1234567890333 | True           | StopSelling     |

Scenario: Add products - red path
	Given Tom enters the following product information:
	| Index | Name | Description     | Price | Retail | Cost | Length | Width | Height | Weight | ShippingType    | Sku      | Barcode       | TrackInventory | OutOfStockRule   | Comment                 |
	| 1     | DDD  | DDD Description | 100   | 120    | 80   | 1      | 2     | 3      | 4      | Ship Separately | ddd-book | 1234567890111 | Yes            | Remove           | Invalid ShippingType    |
	| 2     | BDD  | BDD Description | 90    | 110    | 70   | 2      | 3     | 4      | 5      | PhysicalProduct | bdd-book | 1234567890222 | No             | Continue Selling | Invalid OutOfStock Rule |
	When Tom saves the products
	Then Tom should receive status codes BadRequest
	
Scenario: Update products - green path
	Given Tom already created the following products:
	| Index | Name | Description     | Price | Retail | Cost | Length | Width | Height | Weight | ShippingType    | Sku      | Barcode       | TrackInventory | OutOfStockRule  |
	| 1     | DDD  | DDD Description | 100   | 120    | 80   | 1      | 2     | 3      | 4      | ShipSeparately  | ddd-book | 1234567890111 | True           | Remove          |
	| 2     | BDD  | BDD Description | 90    | 110    | 70   | 2      | 3     | 4      | 5      | PhysicalProduct | bdd-book | 1234567890222 | False          | ContinueSelling |
	| 3     | TDD  | TDD Description | 80    | 100    | 60   | 3      | 4     | 5      | 6      | DigitalProduct  | tdd-book | 1234567890333 | True           | StopSelling     |
	And Tom enters the following product information:
	| Index | Name     | Description | Price | Retail | Cost | Length | Width | Height | Weight | ShippingType    | Sku      | Barcode       | TrackInventory | OutOfStockRule  |
	| 1     | DDD Lite | DDD Desc    | 90    | 110    | 70   | 4      | 3     | 2      | 1      | DigitalProduct  | ddd-lite | 1234567890123 | True           | StopSelling     |
	| 2     | BDD Lite | BDD Desc    | 80    | 100    | 60   | 5      | 4     | 3      | 2      | ShipSeparately  | bdd-lite | 1234567890234 | True           | Remove          |
	| 3     | TDD Lite | TDD Desc    | 70    | 90     | 50   | 6      | 5     | 4      | 3      | PhysicalProduct | tdd-lite | 1234567890345 | False          | ContinueSelling |
	When Tom upates the products
	Then Tom should receive status codes OK
	And the products should be updated as follow:
	| Index | Name | Description     | Price | Retail | Cost | Length | Width | Height | Weight | ShippingType    | Sku      | Barcode       | TrackInventory | OutOfStockRule  |
	| 1     | DDD  | DDD Description | 100   | 120    | 80   | 1      | 2     | 3      | 4      | ShipSeparately  | ddd-book | 1234567890111 | True           | Remove          |
	| 2     | BDD  | BDD Description | 90    | 110    | 70   | 2      | 3     | 4      | 5      | PhysicalProduct | bdd-book | 1234567890222 | False          | ContinueSelling |
	| 3     | TDD  | TDD Description | 80    | 100    | 60   | 3      | 4     | 5      | 6      | DigitalProduct  | tdd-book | 1234567890333 | True           | StopSelling     |

Scenario: Update products - red path
	Given Tom already created the following products:
	| Index | Name | Description     | Price | Retail | Cost | Length | Width | Height | Weight | ShippingType    | Sku      | Barcode       | TrackInventory | OutOfStockRule  |
	| 1     | DDD  | DDD Description | 100   | 120    | 80   | 1      | 2     | 3      | 4      | ShipSeparately  | ddd-book | 1234567890111 | True           | Remove          |
	| 2     | BDD  | BDD Description | 90    | 110    | 70   | 2      | 3     | 4      | 5      | PhysicalProduct | bdd-book | 1234567890222 | False          | ContinueSelling |
	And Tom enters the following product information:
	| Index | Name     | Description | Price | Retail | Cost | Length | Width | Height | Weight | ShippingType    | Sku      | Barcode       | TrackInventory | OutOfStockRule | Comment                 |
	| 1     | DDD Lite | DDD Desc    | 90    | 110    | 70   | 4      | 3     | 2      | 1      | DigitalProduct  | ddd-lite | 1234567890123 | True           | Stop Selling   | Invalid OutOfStock Rule |
	| 2     | BDD Lite | BDD Desc    | 80    | 100    | 60   | 5      | 4     | 3      | 2      | Ship Separately | bdd-lite | 1234567890234 | True           | Remove         | Invalid ShippingType    |
	When Tom upates the products
	Then Tom should receive status codes BadRequest