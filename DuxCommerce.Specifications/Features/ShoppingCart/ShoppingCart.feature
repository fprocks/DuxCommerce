Feature: Shopping Cart
	In order to place an order
	As a customer
	I want to add products to my shopping cart

Background: 
	Given Tom creates the following products:
	| Index | Name | Description     | Price | Retail | Cost | Length | Width | Height | Weight | ProductType     | Sku      | Barcode       | TrackInventory | OutOfStockRule  |
	| 1     | DDD  | DDD Description | 100   | 120    | 80   | 1      | 2     | 3      | 4      | DigitalProduct  | ddd-book | 1234567890111 | True           | Remove          |
	| 2     | BDD  | BDD Description | 50    | 110    | 70   | 2      | 3     | 4      | 5      | PhysicalProduct | bdd-book | 1234567890222 | False          | ContinueSelling |
	| 3     | TDD  | TDD Description | 80    | 100    | 60   | 3      | 4     | 5      | 6      | DigitalProduct  | tdd-book | 1234567890333 | True           | StopSelling     |

Scenario: Add to cart
	When Amy adds the following products to her shopping cart:
	| Product | Name | Quantity |
	| 1       | DDD  | 1        |
	| 1       | DDD  | 2        |
	| 2       | BDD  | 8        |
	Then Amy should receive status codes OK
	And her cart details should look as follow:
	| Product | Name | Price | Quantity | ItemTotal |
	| 1       | DDD  | 100   | 3        | 300       |
	| 2       | BDD  | 50    | 8        | 400       |
	And the cart total is $700

Scenario: Update cart
	And Amy adds the following products to her shopping cart:
	| Product | Name | Quantity |
	| 1       | DDD  | 4        |
	| 2       | BDD  | 8        |
	When Amy updates her shopping cart as follow: 
	| Product | Name | Quantity |
	| 1       | DDD  | 10       |
	| 2       | BDD  | 20       |
	Then Amy should receive status codes OK
	And her cart details should look as follow:
	| Product | Name | Price | Quantity | ItemTotal |
	| 1       | DDD  | 100   | 10       | 1000      |
	| 2       | BDD  | 50    | 20       | 1000      |
	And the cart total is $2000

Scenario: Delete cart item
	And Amy adds the following products to her shopping cart:
	| Product | Name | Quantity |
	| 1       | DDD  | 2        |
	| 2       | BDD  | 4        |
	| 3       | TDD  | 6        |
	When Amy deletes the following cart items: 
	| Product | Name |
	| 1       | DDD  |
	| 3       | TDD  |
	Then Amy should receive status codes OK
	And her cart details should look as follow:
	| Product | Name | Price | Quantity | ItemTotal |
	| 2       | BDD  | 50    | 4        | 200       |
	And the cart total is $200