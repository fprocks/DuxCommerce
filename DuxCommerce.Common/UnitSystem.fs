namespace DuxCommerce.Common

type WeightUnit =
    | Imperial of ImperialWeightUnit
    | Metric of MetricWeightUnit
    
type LengthUnit =
    | Imperial of ImperialLengthUnit
    | Metric of MetricLengthUnit

type UnitSystem =
    | ImperialSystem of ImperialSystem
    | MetricSystem of MetricSystem

