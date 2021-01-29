Feature: Update Products
	In order to keep the product info up to date
	As a store admin
	I want to update the proucts

Background: 
	Given Tom already created the following product:
	| Name | Description     | Price | Retail | Cost | Length | Width | Height | Weight | ProductType    | Sku      | Barcode       | TrackInventory | OutOfStockRule |
	| DDD  | DDD Description | 100   | 120    | 80   | 1      | 2     | 3      | 4      | DigitalProduct | ddd-book | 1234567890111 | True           | Remove         |

Scenario: Update products - green path
	And Tom enters the following product information:
	| Name   | Description   | Price   | Retail   | Cost   | Length   | Width   | Height   | Weight   | ProductType   | Sku   | Barcode   | TrackInventory   | OutOfStockRule   |
	| <Name> | <Description> | <Price> | <Retail> | <Cost> | <Length> | <Width> | <Height> | <Weight> | <ProductType> | <Sku> | <Barcode> | <TrackInventory> | <OutOfStockRule> |
	When Tom updates the product
	Then Tom should receive status codes OK
	And the product should be updated as follow:
	| Name   | Description   | Price   | Retail   | Cost   | Length   | Width   | Height   | Weight   | ProductType   | Sku   | Barcode   | TrackInventory   | OutOfStockRule   |
	| <Name> | <Description> | <Price> | <Retail> | <Cost> | <Length> | <Width> | <Height> | <Weight> | <ProductType> | <Sku> | <Barcode> | <TrackInventory> | <OutOfStockRule> |
Examples: 
	| Name     | Description | Price | Retail | Cost | Length | Width | Height | Weight | ProductType     | Sku      | Barcode       | TrackInventory | OutOfStockRule  |
	| DDD Lite | DDD Desc    | 90    | 110    | 70   | 4      | 3     | 2      | 1      | DigitalProduct  | ddd-lite | 1234567890123 | True           | StopSelling     |
	| TDD Lite | TDD Desc    | 70    | 90     | 50   | 6      | 5     | 4      | 3      | PhysicalProduct | tdd-lite | 1234567890345 | False          | ContinueSelling |

Scenario: Update products - red path
	And Tom enters the following product information:
	| Name   | Description   | Price   | Retail   | Cost   | Length   | Width   | Height   | Weight   | ProductType   | Sku   | Barcode   | TrackInventory   | OutOfStockRule   |
	| <Name> | <Description> | <Price> | <Retail> | <Cost> | <Length> | <Width> | <Height> | <Weight> | <ProductType> | <Sku> | <Barcode> | <TrackInventory> | <OutOfStockRule> |
	When Tom updates the product
	Then Tom should receive status codes BadRequest
Examples: 
	| Name     | Description | Price | Retail | Cost | Length | Width | Height | Weight | ProductType     | Sku      | Barcode       | TrackInventory | OutOfStockRule | Comment                 |
	| DDD Lite | DDD Desc    | 90    | 110    | 70   | 4      | 3     | 2      | 1      | DigitalProduct  | ddd-lite | 1234567890123 | True           | Stop Selling   | Invalid OutOfStock Rule |
	| BDD Lite | BDD Desc    | 80    | 100    | 60   | 5      | 4     | 3      | 2      | Digital Product | bdd-lite | 1234567890234 | True           | Remove         | Invalid ProductType     |