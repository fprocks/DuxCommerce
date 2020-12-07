namespace DuxCommerce.Settings

open DuxCommerce.Settings.Dto
open DuxCommerce.Settings.Ports
open DuxCommerce.Common

module UseCases =     
    let createStoreProfile :CreateStoreProfileUseCase =            
        fun storeDto ->
            readerResult {
                let! _ = 
                    storeDto 
                    |> StoreProfileDto.toDomain 
                    |> CustomError.mapValidation 
                    |> ConfigReader.retn
                               
                let! storeId = storeDto |> StoreProfileRepo.createStoreProfile
                let! _ = storeDto.Address |> LocationRepo.createLocation

                return! StoreProfileRepo.getStoreProfile storeId 
            }
    
    let getStoreProfile :GetStoreProfileUseCase =
        fun id ->
            readerResult {                
                return! StoreProfileRepo.getStoreProfile id
            }
        
    let updateStoreProfile :UpdateStoreProfileUseCase =
        fun id storeDto ->
            readerResult {
                let! _ = 
                    storeDto
                    |> StoreProfileDto.toDomain
                    |> CustomError.mapValidation
                    |> ConfigReader.retn

                let! details = StoreProfileRepo.getStoreProfile id
                let storeDto = {storeDto with AddressId = details.AddressId}                    
                do! StoreProfileRepo.updateStoreProfile id storeDto

                return! StoreProfileRepo.getStoreProfile id
            }