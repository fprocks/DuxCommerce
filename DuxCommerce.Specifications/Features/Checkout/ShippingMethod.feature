Feature: Add Shipping Method
	In order to get my order delivered
	As an online shopper
	I want to choose a shipping method for my order

Background:
	Given Tom creates the following shipping origins:
	| OriginId | FirstName | LastName | AddressLine1    | AddressLine2 | City   | PostalCode | StateName       | CountryCode |
	| 1        | James     | Harper   | 1 Market Street |              | Sydney | 2000       | New South Wales | AU          |
	And Tom enters shipping profile name Heavy Products
	And Tom selects shipping origin 1
	And Tom enters the zone name ANZ
	And Tom selects the following shipping countries:
	| CountryCode |
	| AU          |
	| NZ          |
	And Tom selects the following shipping states:
	| CountryCode | Name            |
	| AU          | New South Wales |
	| AU          | Queensland      |
	| NZ          | Auckland        |
	| NZ          | Wellington      |
	And Tom enters the following shipping methods:
	| Index | MethodType   | Name           |
	| 1     | ByWeight     | By Weight      |
	| 2     | ByQuantity   | By Quantity    |
	| 3     | ByOrderTotal | By Order Total |
	And Tom enters the following shipping rates:
	| ShippingMethod | Min | Max    | Rate |
	| 1              | 0   | 100    | 50   |
	| 1              | 100 | 200    | 100  |
	| 1              | 200 | 100000 | 200  |
	| 2              | 0   | 10     | 50   |
	| 2              | 10  | 20     | 100  |
	| 2              | 20  | 100000 | 200  |
	| 3              | 0   | 50     | 50   |
	| 3              | 50  | 500    | 100  |
	| 3              | 500 | 100000 | 200  |
	And Tom saves the shipping profile 
	And Tom creates the following product:
	| Index | Name | Description     | Price | Retail | Cost | Length | Width | Height | Weight | ProductType     | Sku      | Barcode       | TrackInventory | OutOfStockRule  |
	| 1     | DDD  | DDD Description | 100   | 120    | 80   | 1      | 2     | 3      | 4      | DigitalProduct  | ddd-book | 1234567890111 | True           | Remove          |
	| 2     | BDD  | BDD Description | 50    | 110    | 70   | 2      | 3     | 4      | 5      | PhysicalProduct | bdd-book | 1234567890222 | False          | ContinueSelling |
	| 3     | TDD  | TDD Description | 80    | 100    | 60   | 3      | 4     | 5      | 6      | DigitalProduct  | tdd-book | 1234567890333 | True           | StopSelling     |

Scenario: Add Shipping Method
	Given Amy adds the following products to her shopping cart:
	| Product | Name | Quantity |
	| 1       | DDD  | 4        |
	| 2       | BDD  | 8        |
	And Amy enters the email address amy@gmail.com
	And Amy enters the following shipping address
	| FirstName | LastName | AddressLine1 | AddressLine2    | City      | PostalCode | StateName | CountryCode |
	| James     | Harper   | Unit 7       | 2 Market Street | Melbourne | 3000       | Victoria  | AU          |
	And Amy saves her contact details and shipping address
	#When Amy selects shipping method 2
	#And Amy saves her shipping method
	#Then Amy should receive status codes OK
	#And checkout information should be saved as expected