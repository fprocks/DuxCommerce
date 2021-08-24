namespace DuxCommerce.Core.Taxes

open DuxCommerce.Core.Taxes.PublicTypes
open DuxCommerce.Common
open DuxCommerce.Core.Taxes.Dto
open DuxCommerce.Core.Taxes.MongoRepos

module TaxRateUseCases =

    type CreateTaxRateUseCase = TaxRateDto -> ConfigReader<Result<TaxRateDto, CustomError>>

    let createRate: CreateTaxRateUseCase =
        fun rateDto ->
            readerResult {
                let! _ =
                    rateDto
                    |> TaxRateDto.toDomain
                    |> CustomError.mapValidation
                    |> ConfigReader.retn

                let! rateId = rateDto |> TaxRateRepo.createRate

                return! TaxRateRepo.getRate rateId
            }
