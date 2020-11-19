namespace DuxCommerce.Settings

open DuxCommerce.Settings.Ports
open DuxCommerce.Common
open DuxCommerce.Settings.PublicTypes

module UseCases =     

    let createStoreDetails :CreateStoreDetailsUseCase =            
        fun storeDetailsDto ->
            readerResult {
                let! product = storeDetailsDto |> StoreDetailsDto.toDomain |> ConfigReader.retn
                let! id = storeDetailsDto |> StoreDetailsRepo.createStoreDetails
                return! StoreDetailsRepo.getStoreDetails id 
            }
    
    let getStoreDetails :GetStoreDetailsUseCase =
        fun id ->
            readerResult {                
                // Todo: improve to handle null value
                return! StoreDetailsRepo.getStoreDetails id
            }
        
    let updateStoreDetails :UpdateStoreDetailsUseCase =
        fun id storeDetailsDto ->
            readerResult {
                let! storeDetails = storeDetailsDto |> StoreDetailsDto.toDomain |> ConfigReader.retn
                do! StoreDetailsRepo.updateStoreDetails id storeDetailsDto
                return! StoreDetailsRepo.getStoreDetails id
            }