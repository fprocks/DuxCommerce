Feature: Add Shipping Method
	In order to get my order delivered
	As an online shopper
	I want to choose a shipping method for my order
#
#Background: 
#	Given the following products are already created:
#	| Index | Name | Description     | Price | Retail | Cost | Length | Width | Height | Weight | ProductType     | Sku      | Barcode       | TrackInventory | OutOfStockRule  |
#	| 1     | DDD  | DDD Description | 100   | 120    | 80   | 1      | 2     | 3      | 4      | DigitalProduct  | ddd-book | 1234567890111 | True           | Remove          |
#	| 2     | BDD  | BDD Description | 50    | 110    | 70   | 2      | 3     | 4      | 5      | PhysicalProduct | bdd-book | 1234567890222 | False          | ContinueSelling |
#	| 3     | TDD  | TDD Description | 80    | 100    | 60   | 3      | 4     | 5      | 6      | DigitalProduct  | tdd-book | 1234567890333 | True           | StopSelling     |
#	And Amy adds the following products to her shopping cart:
#	| Product | Name | Quantity |
#	| 1       | DDD  | 4        |
#	| 2       | BDD  | 8        |
#	And Tom already created the following shipping origins:
#	| OriginId | FirstName | LastName | AddressLine1    | AddressLine2 | City   | PostalCode | StateName       | CountryCode |
#	| 1        | James     | Harper   | 1 Market Street |              | Sydney | 2000       | New South Wales | AU          |
#	And Tom enters shipping profile name Heavy Products
#	And Tom selects shipping origin 1
#	And Tom enters the zone name ANZ
#	And Tom selects the following shipping countries:
#	| CountryCode |
#	| AU          |
#	| NZ          |
#	And Tom selects the following shipping states:
#	| CountryCode | Name            |
#	| AU          | New South Wales |
#	| AU          | Queensland      |
#	| NZ          | Auckland        |
#	| NZ          | Wellington      |
#	And Tom creates shipping method 1 with <MethodType> and <MethodName>
#	And Tome enters the following rates:
#	| Min | Max   | Rate |
#	| 0   | 100   | 50   |
#	| 100 | 200   | 100  |
#	| 200 | 40000 | 200  |
#	And Tom saves the shipping profile
#
#Examples: 
#	| MethodType   | MethodName     |
#	| ByWeight     | By Weight      |
#	| ByQuantity   | By Quantity    |
#	| ByOrderTotal | By Order Total |
#
#Scenario: Add Customer Information
#	And Amy enters the email address amy@gmail.com
#	And Amy enters the following shipping address
#	| FirstName | LastName | AddressLine1 | AddressLine2    | City      | PostalCode | StateName | CountryCode |
#	| James     | Harper   | Unit 7       | 2 Market Street | Melbourne | 3000       | Victoria  | AU          |
#	When Amy saves her contact details and shipping address
#	Then Amy should receive status codes OK
#	Then Amy's information should be saved as expected