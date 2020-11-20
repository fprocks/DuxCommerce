namespace DuxCommerce.Common

open DuxCommerce.Common                   
type UnitSystem =
    | ImperialSystem
    | MetricSystem
        
module UnitSystem =
    let value system =
        match system with
        | ImperialSystem _ -> "ImperialSystem"
        | MetricSystem _ -> "MetricSystem"
                 
    let create system =
        match system with
        | "ImperialSystem" -> Ok ImperialSystem
        | "MetricSystem" -> Ok MetricSystem
        | _ ->
            let msg = "UnitSystem must be 'ImperialSystem' or 'MetricSystem'" 
            Error msg
            
type WeightUnit =
    | Imperial of ImperialWeightUnit
    | Metric of MetricWeightUnit
    
module WeightUnit =
    let value weight =
        match weight with
        | Imperial Pound -> "Pound"
        | Imperial Ounce -> "Ounce"
        | Metric Kilogram -> "Kilogram"
        | Metric Gram -> "Gram"
        
    let create (system:UnitSystem) weight =
        match system, weight with
        | ImperialSystem, "Pound" -> Ok (Imperial Pound)
        | ImperialSystem, "Ounce" -> Ok (Imperial Ounce)
        | ImperialSystem, _ -> Error "ImperialWeightUnit must be 'Pound' or 'Ounce'" 
        | MetricSystem, "Kilogram" -> Ok (Metric Kilogram)
        | MetricSystem, "Gram" -> Ok (Metric Gram)
        | MetricSystem, _ -> Error "MetricWeightUnit must be 'Kilogram' or 'Gram'" 
        
type LengthUnit =
    | Imperial of ImperialLengthUnit
    | Metric of MetricLengthUnit
    
module LengthUnit =
    let value length =
        match length with
        | Imperial Inch -> "Inch"
        | Imperial Foot -> "Foot"
        | Metric Centimeter -> "Centimeter"
        | Metric Meter -> "Meter"
        
    let create system lengthUnit =
        match system, lengthUnit with
        | ImperialSystem, "Inch" -> Ok (Imperial Inch)
        | ImperialSystem, "Foot" -> Ok (Imperial Foot)
        | ImperialSystem, _ -> Error "ImperialLengthUnit must be 'Inch' or 'Foot'" 
        | MetricSystem, "Centimeter" -> Ok (Metric Centimeter)
        | MetricSystem, "Meter" -> Ok (Metric Meter)
        | MetricSystem, _ -> Error "MetricLengthUnit must be 'Centimeter' or 'Meter'" 