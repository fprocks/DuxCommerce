Feature: Create a product
	In order to sell products online
	As a store admin
	I want to add proucts to my store first

Scenario: Create products - green path
	Given Tom enters the following product information:
	| Name   | Description   | Price    | Retail   | Cost   | Length   | Width   | Height   | Weight   | ProductType   | Sku   | Barcode   | TrackInventory   | OutOfStockRule   |
	| <Name> | <Description> | <Price> | <Retail> | <Cost> | <Length> | <Width> | <Height> | <Weigth> | <ProductType> | <Sku> | <Barcode> | <TrackInventory> | <OutOfStockRule> |
	When Tom saves the product
	Then Tom should receive status codes OK
	And the product should be created as follow:
	| Name   | Description   | Price    | Retail   | Cost   | Length   | Width   | Height   | Weight   | ProductType   | Sku   | Barcode   | TrackInventory   | OutOfStockRule   |
	| <Name> | <Description> | <Price> | <Retail> | <Cost> | <Length> | <Width> | <Height> | <Weigth> | <ProductType> | <Sku> | <Barcode> | <TrackInventory> | <OutOfStockRule> |
Examples: 
	| Name | Description     | Price | Retail | Cost | Length | Width | Height | Weight | ProductType     | Sku      | Barcode       | TrackInventory | OutOfStockRule  |
	| BDD  | BDD Description | 90    | 110    | 70   | 2      | 3     | 4      | 5      | PhysicalProduct | bdd-book | 1234567890222 | False          | ContinueSelling |
	| TDD  | TDD Description | 80    | 100    | 60   | 3      | 4     | 5      | 6      | DigitalProduct  | tdd-book | 1234567890333 | True           | StopSelling     |

Scenario: Create products - red path
	Given Tom enters the following product information:
	| Name   | Description   | Price    | Retail   | Cost   | Length   | Width   | Height   | Weight   | ProductType   | Sku   | Barcode   | TrackInventory   | OutOfStockRule   |
	| <Name> | <Description> | <Price> | <Retail> | <Cost> | <Length> | <Width> | <Height> | <Weigth> | <ProductType> | <Sku> | <Barcode> | <TrackInventory> | <OutOfStockRule> |
	When Tom saves the product
	Then Tom should receive status codes BadRequest
Examples: 
	| Name | Description     | Price | Retail | Cost | Length | Width | Height | Weight | ProductType      | Sku      | Barcode       | TrackInventory | OutOfStockRule   | Comment                 |
	| DDD  | DDD Description | 100   | 120    | 80   | 1      | 2     | 3      | 4      | Physical Product | ddd-book | 1234567890111 | Yes            | Remove           | Invalid Product Type    |
	| BDD  | BDD Description | 90    | 110    | 70   | 2      | 3     | 4      | 5      | PhysicalProduct  | bdd-book | 1234567890222 | No             | Continue Selling | Invalid OutOfStock Rule |
