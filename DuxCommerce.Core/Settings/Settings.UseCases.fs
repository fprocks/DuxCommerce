namespace DuxCommerce.Settings

open DuxCommerce.Settings.Dto
open DuxCommerce.Settings.Ports
open DuxCommerce.Settings.MongoRepos
open DuxCommerce.Common

module StoreProfileUseCases =   

    let createProfile :CreateStoreProfileUseCase =            
        fun profileDto ->
            readerResult {
                let! _ = profileDto 
                        |> StoreProfileDto.toDomain 
                        |> CustomError.mapValidation 
                        |> ConfigReader.retn
                               
                let! profileId = profileDto |> StoreProfileRepo.createProfile

                // Todo: why the next line never executes ShippingProfileRepo.createProfile
                //profileDto.Address |> ShippingProfileRepo.createProfile |> ignore

                let! _ = profileDto.Address |> ShippingProfileRepo.createDefaultProfile

                return! StoreProfileRepo.getProfile profileId 
            }
    
    let getProfile :GetStoreProfileUseCase =
        fun id ->
            readerResult {                
                return! StoreProfileRepo.getProfile id
            }
        
    let updateProfile :UpdateStoreProfileUseCase =
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

module ShippingProfileUseCases =

    let getDefaultProfile :GetDefaultProfileUseCase =
        fun () ->
            readerResult {
                return! ShippingProfileRepo.getDefault ()
            }

    let createProfile :CreateShippingProfileUseCase = 
        fun dto -> 
            readerResult {
                let! _ = dto
                        |> ShippingProfileDto.toDomain
                        |> CustomError.mapValidation
                        |> ConfigReader.retn

                let! profileId = ShippingProfileRepo.createCustomProfile dto
                return! ShippingProfileRepo.getProfile profileId
            }

module ShippingOriginUseCases =

    let createOrigin :CreateShippingOriginUseCase = 
        fun addressDto ->
            readerResult {
                let! originId = addressDto |> ShippingOriginRepo.createOrigin
                return! ShippingOriginRepo.getOrigin originId
            }    