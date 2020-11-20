namespace DuxCommerce.Settings

open DuxCommerce.Settings.Dto
open DuxCommerce.Settings.Ports
open DuxCommerce.Common

module UseCases =     

    let createStoreDetails :CreateStoreDetailsUseCase =            
        fun storeDetailsDto ->
            readerResult {
                let! _ = storeDetailsDto
                               |> StoreDetailsDto.toDomain
                               |> CustomError.mapValidation
                               |> ConfigReader.retn
                               
                let! id = storeDetailsDto |> StoreDetailsRepo.createStoreDetails
                return! StoreDetailsRepo.getStoreDetails id 
            }
    
    let getStoreDetails :GetStoreDetailsUseCase =
        fun id ->
            readerResult {                
                return! StoreDetailsRepo.getStoreDetails id
            }
        
    let updateStoreDetails :UpdateStoreDetailsUseCase =
        fun id storeDetailsDto ->
            readerResult {
                let! _ = storeDetailsDto
                                    |> StoreDetailsDto.toDomain
                                    |> CustomError.mapValidation
                                    |> ConfigReader.retn
                                    
                do! StoreDetailsRepo.updateStoreDetails id storeDetailsDto
                return! StoreDetailsRepo.getStoreDetails id
            }