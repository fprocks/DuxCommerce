Feature: ShoppingCart
	In order to place an order
	As a customer
	I want to add products to my shopping cart

Scenario: Add to cart
	Given the following products are already created:
	| Index | Name | Description     | Price | Retail | Cost | Length | Width | Height | Weight | ShippingType    | Sku      | Barcode       | TrackInventory | OutOfStockRule  |
	| 1     | DDD  | DDD Description | 100   | 120    | 80   | 1      | 2     | 3      | 4      | ShipSeparately  | ddd-book | 1234567890111 | True           | Remove          |
	| 2     | BDD  | BDD Description | 50    | 110    | 70   | 2      | 3     | 4      | 5      | PhysicalProduct | bdd-book | 1234567890222 | False          | ContinueSelling |
	| 3     | TDD  | TDD Description | 80    | 100    | 60   | 3      | 4     | 5      | 6      | DigitalProduct  | tdd-book | 1234567890333 | True           | StopSelling     |
	When Amy adds the following products to her cart:
	| Product | Name | Quantity |
	| 1       | DDD  | 1        |
	| 1       | DDD  | 2        |
	| 2       | BDD  | 8        |
	Then the shopping cart details should look like following:
	| Product | Name | Price | Quantity | ItemTotal |
	| 1       | DDD  | 100   | 3        | 300       |
	| 2       | BDD  | 50    | 8        | 400       |
	And the cart total is $700