namespace DuxCommerce.Settings.PublicTypes

[<CLIMutable>]
type ShippingCountryRequest = {
    CountryCode: string
    States: int64 seq
}

[<CLIMutable>]
type ShippingRateItemRequest = {
    Min: decimal
    Max: decimal
    Rate: decimal
}

[<CLIMutable>]
type ShippingRateRequest = {
    Name: string
    Ratetype: string
    Items: ShippingRateItemRequest seq
}

[<CLIMutable>]
type ShippingZoneRequest = {
    Name: string
    Rates: ShippingRateRequest seq
    Countries: ShippingCountryRequest seq
}

[<CLIMutable>]
type ShippingProfileRequest = {
    Name: string
    Origins: int64 seq
    Zones: ShippingZoneRequest seq
}