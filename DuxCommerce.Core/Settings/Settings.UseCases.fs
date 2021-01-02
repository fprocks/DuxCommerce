namespace DuxCommerce.Settings

open DuxCommerce.Settings.Dto
open DuxCommerce.Settings.Ports
open DuxCommerce.Common

module UseCases =     
    let createStoreProfile :CreateStoreProfileUseCase =            
        fun profileDto ->
            readerResult {
                let! _ = profileDto 
                        |> StoreProfileDto.toDomain 
                        |> CustomError.mapValidation 
                        |> ConfigReader.retn
                               
                let! profileId = profileDto |> StoreProfileRepo.createProfile
                let! originId = profileDto.Address |> ShippingOriginRepo.createOrigin

                // Todo: why the next line never executes ShippingProfileRepo.createProfile
                //profileDto.Address |> ShippingProfileRepo.createProfile originId |> ignore

                let! _ = profileDto.Address |> ShippingProfileRepo.createProfile originId

                return! StoreProfileRepo.getProfile profileId 
            }
    
    let getStoreProfile :GetStoreProfileUseCase =
        fun id ->
            readerResult {                
                return! StoreProfileRepo.getProfile id
            }
        
    let updateStoreProfile :UpdateStoreProfileUseCase =
        fun id profileDto ->
            readerResult {
                let! _ = profileDto
                        |> StoreProfileDto.toDomain
                        |> CustomError.mapValidation
                        |> ConfigReader.retn

                let! profile = StoreProfileRepo.getProfile id
                let updatedProfile = {profileDto with AddressId = profile.AddressId}                    
                do! StoreProfileRepo.updateProfile id updatedProfile

                return! StoreProfileRepo.getProfile id
            }
            
    let getDefaultShippingProfile :GetDefaultProfileUseCase =
        fun () ->
            readerResult {
                return! ShippingProfileRepo.getDefault ()
            }

    let createShippingProfile :CreateShippingProfileUseCase = 
        fun dto -> 
            readerResult {
                let! _ = dto
                        |> ShippingProfileDto.toDomain
                        |> CustomError.mapValidation
                        |> ConfigReader.retn

                let! profileId = ShippingProfileRepo.createProfile2 dto
                return! ShippingProfileRepo.getProfile profileId
            }

    let createShippingOrigin :CreateShippingOriginUseCase = 
        fun addressDto ->
            readerResult {
                let! originId = addressDto |> ShippingOriginRepo.createOrigin
                return! ShippingOriginRepo.getOrigin originId
            }    