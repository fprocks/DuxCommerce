namespace DuxCommerce.Common

// Todo: how to implement unit systems using built-in units of measure

type Pound = private Pound of string
type Ounce = private Ounce of string

type ImperialWeightUnit =
    | Pound of Pound
    | Ounce of Ounce
       
type Inch = private Inch of string
type Foot = private Foot of string

type ImperialLengthUnit =
    | Inch of Inch
    | Foot of Foot

type Kilogram = private Kilogram of string
type Gram = private Gram of string

type ImperialSystem = {
    WeightUnit : ImperialWeightUnit
    LengthUnit : ImperialLengthUnit
}
    

