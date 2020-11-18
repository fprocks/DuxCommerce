namespace DuxCommerce.Common

type MetricWeightUnit =
    | Kilogram of Kilogram
    | Gram of Gram
    
type Centimeter = private Centimeter of string
type Meter = private Meter of string

type MetricLengthUnit =
    | Meter of Meter
    | Centimeter of Centimeter
    
type MetricSystem = {
    WeightUnit : MetricWeightUnit
    LengthUnit : MetricLengthUnit
}