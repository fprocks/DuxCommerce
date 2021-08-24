namespace DuxCommerce.Core.Taxes.Dto

open DuxCommerce.Common
open DuxCommerce.Core.Taxes.PublicTypes
open DuxCommerce.Core.Taxes.InternalTypes
open DuxCommerce.Core.Taxes.SimpleTypes
open DuxCommerce.Core.Shared.SimpleTypes
open DuxCommerce.Core.Shared.Dto

module TaxCountryDto =

    let toDomain (dto: TaxCountryDto) : Result<TaxCountry, string> =
        result {
            let! countryCode = CountryCode.create "CountryCode" dto.CountryCode

            let! states =
                dto.States
                |> Seq.map StateDto.toDomain
                |> Seq.toList
                |> Result.sequence

            let! postalCodes =
                dto.PostalCodes
                |> Seq.map (String50.create "PostalCode")
                |> Seq.toList
                |> Result.sequence

            return
                { CountryCode = countryCode
                  States = states
                  PostalCodes = postalCodes }
        }

module TaxZoneDto =

    let toDomain (zoneDto: TaxZoneDto) : Result<TaxZone, string> =
        result {
            let! name = String50.create "ZoneName" zoneDto.Name

            let! countries =
                zoneDto.Countries
                |> Seq.map TaxCountryDto.toDomain
                |> Seq.toList
                |> Result.sequence

            return { Name = name; Countries = countries }
        }

module TaxRateDto =

    let toDomain (rateDto: TaxRateDto) : Result<TaxRate, string> =
        result {
            let! name = String50.create "RateName" rateDto.Name
            let amount = TaxRateAmount.create rateDto.Amount
            let! zone = rateDto.Zone |> TaxZoneDto.toDomain

            return
                { TaxRateId = TaxRateId.create ""
                  Name = name
                  Amount = amount
                  Zone = zone }
        }
