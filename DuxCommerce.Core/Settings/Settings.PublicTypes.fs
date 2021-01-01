namespace DuxCommerce.Settings.PublicTypes

[<CLIMutable>]
type ShippingCountryRequest = {
    CountryCode: string
    States: int64 seq
}

[<CLIMutable>]
type ShippingRateRequest = {
    Min: decimal
    Max: decimal
    Rate: decimal
}

[<CLIMutable>]
type ShippingMethodRequest = {
    Name: string
    MethodType: string
    Rates: ShippingRateRequest seq
}

[<CLIMutable>]
type ShippingZoneRequest = {
    Name: string
    Methods: ShippingMethodRequest seq
    Countries: ShippingCountryRequest seq
}

[<CLIMutable>]
type ShippingProfileRequest = {
    Name: string
    Origins: int64 seq
    Zones: ShippingZoneRequest seq
}