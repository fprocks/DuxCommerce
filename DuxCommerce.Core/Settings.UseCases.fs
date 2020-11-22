namespace DuxCommerce.Settings

open DuxCommerce.Settings.Dto
open DuxCommerce.Settings.Ports
open DuxCommerce.Common

module UseCases =     
    let createStoreDetails :CreateStoreDetailsUseCase =            
        fun detailsDto ->
            readerResult {
                let! _ = 
                    detailsDto 
                    |> StoreDetailsDto.toDomain 
                    |> CustomError.mapValidation 
                    |> ConfigReader.retn
                               
                let! id = detailsDto |> StoreDetailsRepo.createStoreDetails
                return! StoreDetailsRepo.getStoreDetails id 
            }
    
    let getStoreDetails :GetStoreDetailsUseCase =
        fun id ->
            readerResult {                
                return! StoreDetailsRepo.getStoreDetails id
            }
        
    let updateStoreDetails :UpdateStoreDetailsUseCase =
        fun id detailsDto ->
            readerResult {
                let! _ = 
                    detailsDto
                    |> StoreDetailsDto.toDomain
                    |> CustomError.mapValidation
                    |> ConfigReader.retn

                let! details = StoreDetailsRepo.getStoreDetails id
                let dto = {detailsDto with AddressId = details.AddressId}                    
                do! StoreDetailsRepo.updateStoreDetails id dto

                return! StoreDetailsRepo.getStoreDetails id
            }