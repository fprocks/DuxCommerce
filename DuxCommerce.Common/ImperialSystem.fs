namespace DuxCommerce.Common

type ImperialWeightUnit =
    | Pound
    | Ounce
       
type ImperialLengthUnit =
    | Inch
    | Foot

type ImperialSystem = {
    WeightUnit : ImperialWeightUnit
    LengthUnit : ImperialLengthUnit
}
    

