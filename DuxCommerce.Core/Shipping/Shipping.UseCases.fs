namespace DuxCommerce.Core.Shipping

open DuxCommerce.Core.Shipping.Dto
open DuxCommerce.Core.Shipping.MongoRepos
open DuxCommerce.Common
open DuxCommerce.Core.Shipping.PublicTypes
open DuxCommerce.Core.Shared
open DuxCommerce.Core.Shared.PublicTypes

module StoreProfileUseCases =   

    type CreateStoreProfileUseCase = StoreProfileDto -> ConfigReader<Result<StoreProfileDto, CustomError>>
    let createProfile :CreateStoreProfileUseCase =            
        fun profileDto ->
            readerResult {
                let! _ = 
                    profileDto 
                    |> StoreProfileDto.toDomain 
                    |> CustomError.mapValidation 
                    |> ConfigReader.retn
                               
                let! profileId = 
                    profileDto 
                    |> StoreProfileRepo.createProfile

                let addressDto = {profileDto.Address with Id = ""}
                // Todo: why the next line never executes ShippingProfileRepo.createProfile
                //profileDto.Address |> ShippingProfileRepo.createProfile |> ignore
                let! _ = 
                    addressDto 
                    |> ShippingProfileRepo.createDefaultProfile

                return! StoreProfileRepo.getProfile profileId 
                }

    type GetStoreProfileUseCase = string -> ConfigReader<Result<StoreProfileDto, CustomError>>  
    let getProfile :GetStoreProfileUseCase =
        fun id ->
            readerResult {                
                return! StoreProfileRepo.getProfile id
            }
        
    type UpdateStoreProfileUseCase = string -> StoreProfileDto -> ConfigReader<Result<StoreProfileDto, CustomError>>  
    let updateProfile :UpdateStoreProfileUseCase =
        fun id profileDto ->
            readerResult {
                let! _ = 
                    profileDto
                    |> StoreProfileDto.toDomain
                    |> CustomError.mapValidation
                    |> ConfigReader.retn

                let! profile = StoreProfileRepo.getProfile id
                let updatedProfile = {profileDto with AddressId = profile.AddressId}                    
                do! StoreProfileRepo.updateProfile id updatedProfile

                return! StoreProfileRepo.getProfile id
                }


module ShippingProfileUseCases =

    type GetDefaultProfileUseCase = unit -> ConfigReader<Result<ShippingProfileDto, CustomError>>
    let getDefaultProfile :GetDefaultProfileUseCase =
        fun () ->
            readerResult {
                return! ShippingProfileRepo.getDefaultProfile ()
                }

    type CreateShippingProfileUseCase = ShippingProfileDto -> ConfigReader<Result<ShippingProfileDto, CustomError>>
    let createProfile :CreateShippingProfileUseCase = 
        fun dto -> 
            readerResult {
                let! _ = 
                    dto
                    |> ShippingProfileDto.toDomain
                    |> CustomError.mapValidation
                    |> ConfigReader.retn

                let! profileId = ShippingProfileRepo.createCustomProfile dto
                return! ShippingProfileRepo.getProfile profileId
                }


module ShippingOriginUseCases =

    type CreateShippingOriginUseCase = AddressDto -> ConfigReader<Result<ShippingOriginDto, CustomError>>
    let createOrigin :CreateShippingOriginUseCase = 
        fun addressDto ->
            readerResult {
                let! originId = 
                    addressDto 
                    |> ShippingOriginRepo.createOrigin

                return! ShippingOriginRepo.getOrigin originId
                }

    type GetShippingOriginsUseCase = string seq -> ConfigReader<Result<ShippingOriginDto seq, CustomError>>
    let getOrigins :GetShippingOriginsUseCase = 
        fun ids ->
            readerResult {
                return! ShippingOriginRepo.getOrigins ids
                }