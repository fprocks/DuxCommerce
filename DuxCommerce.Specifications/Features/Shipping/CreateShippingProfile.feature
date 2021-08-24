Feature: Create Shipping Profile
In order to customize my online store
As a store admin
I want to create my shipping profile

    Background:
        Given Tom creates the following shipping origins:
          | OriginId | FirstName | LastName | AddressLine1    | AddressLine2 | City   | PostalCode | StateName       | CountryCode |
          | 1        | James     | Harper   | 1 Market Street |              | Sydney | 2000       | New South Wales | AU          |

    Scenario: Create custom shipping profile
        Given Tom enters shipping profile name Heavy Products
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
          | ShippingMethodId | MethodType   | Name           |
          | 1                | ByWeight     | By Weight      |
          | 2                | ByQuantity   | By Quantity    |
          | 3                | ByOrderTotal | By Order Total |
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
        When Tom saves the shipping profile
        Then Tom should receive status codes OK
        And custom shipping profile should be created as expected