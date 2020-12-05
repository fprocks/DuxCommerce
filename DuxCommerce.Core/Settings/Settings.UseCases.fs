namespace DuxCommerce.Settings

open DuxCommerce.Settings.Dto
open DuxCommerce.Settings.Ports
open DuxCommerce.Common

module UseCases =     
    let createStoreDetails :CreateStoreDetailsUseCase =            
        fun storeDto ->
            readerResult {
                let! _ = 
                    storeDto 
                    |> StoreDetailsDto.toDomain 
                    |> CustomError.mapValidation 
                    |> ConfigReader.retn
                               
                let! storeId = storeDto |> StoreDetailsRepo.createStoreDetails
                let! _ = storeDto.Address |> LocationRepo.createLocation

                return! StoreDetailsRepo.getStoreDetails storeId 
            }
    
    let getStoreDetails :GetStoreDetailsUseCase =
        fun id ->
            readerResult {                
                return! StoreDetailsRepo.getStoreDetails id
            }
        
    let updateStoreDetails :UpdateStoreDetailsUseCase =
        fun id storeDto ->
            readerResult {
                let! _ = 
                    storeDto
                    |> StoreDetailsDto.toDomain
                    |> CustomError.mapValidation
                    |> ConfigReader.retn

                let! details = StoreDetailsRepo.getStoreDetails id
                let storeDto = {storeDto with AddressId = details.AddressId}                    
                do! StoreDetailsRepo.updateStoreDetails id storeDto

                return! StoreDetailsRepo.getStoreDetails id
            }