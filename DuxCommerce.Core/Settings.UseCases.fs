namespace DuxCommerce.Settings

open DuxCommerce.Settings.Dto
open DuxCommerce.Settings.Ports
open DuxCommerce.Common

module UseCases =     
    let createStoreDetails :CreateStoreDetailsUseCase =            
        fun dto ->
            readerResult {
                let! _ = dto
                               |> StoreDetailsDto.toDomain
                               |> CustomError.mapValidation
                               |> ConfigReader.retn
                               
                let! id = dto |> StoreDetailsRepo.createStoreDetails
                return! StoreDetailsRepo.getStoreDetails id 
            }
    
    let getStoreDetails :GetStoreDetailsUseCase =
        fun id ->
            readerResult {                
                return! StoreDetailsRepo.getStoreDetails id
            }
        
    let updateStoreDetails :UpdateStoreDetailsUseCase =
        fun id dto ->
            readerResult {
                let! _ = dto
                                    |> StoreDetailsDto.toDomain
                                    |> CustomError.mapValidation
                                    |> ConfigReader.retn
                                    
                do! StoreDetailsRepo.updateStoreDetails id dto
                return! StoreDetailsRepo.getStoreDetails id
            }