namespace DuxCommerce.Common

type MetricWeightUnit =
    | Kilogram
    | Gram
    
type MetricLengthUnit =
    | Meter
    | Centimeter
    
type MetricSystem = {
    WeightUnit : MetricWeightUnit
    LengthUnit : MetricLengthUnit
}