namespace DuxCommerce.Settings

open DuxCommerce.Settings.Dto
open DuxCommerce.Settings.Ports
open DuxCommerce.Common

module UseCases =     
    let createStoreProfile :CreateStoreProfileUseCase =            
        fun profileDto ->
            readerResult {
                let! _ = 
                    profileDto 
                    |> StoreProfileDto.toDomain 
                    |> CustomError.mapValidation 
                    |> ConfigReader.retn
                               
                let! profileId = profileDto |> StoreProfileRepo.createStoreProfile
                let! originId = profileDto.Address |> ShippingOriginRepo.createOrigin

                // Todo: the following line never executes ShippingProfileRepo.createProfile
                //profileDto.Address |> ShippingProfileRepo.createProfile originId |> ignore

                let! _ = profileDto.Address |> ShippingProfileRepo.createProfile originId

                return! StoreProfileRepo.getStoreProfile profileId 
            }
    
    let getStoreProfile :GetStoreProfileUseCase =
        fun id ->
            readerResult {                
                return! StoreProfileRepo.getStoreProfile id
            }
        
    let updateStoreProfile :UpdateStoreProfileUseCase =
        fun id profileDto ->
            readerResult {
                let! _ = 
                    profileDto
                    |> StoreProfileDto.toDomain
                    |> CustomError.mapValidation
                    |> ConfigReader.retn

                let! profile = StoreProfileRepo.getStoreProfile id
                let updatedProfile = {profileDto with AddressId = profile.AddressId}                    
                do! StoreProfileRepo.updateStoreProfile id updatedProfile

                return! StoreProfileRepo.getStoreProfile id
            }