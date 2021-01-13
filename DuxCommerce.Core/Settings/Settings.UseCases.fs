namespace DuxCommerce.Settings

open DuxCommerce.Settings.Dto
open DuxCommerce.Settings.MongoRepos
open DuxCommerce.Common
open DuxCommerce.Settings.PublicTypes

type CreateStoreProfileUseCase = StoreProfileDto -> ConfigReader<Result<StoreProfileDto, CustomError>>
type GetStoreProfileUseCase = string -> ConfigReader<Result<StoreProfileDto, CustomError>>
type UpdateStoreProfileUseCase = string -> StoreProfileDto -> ConfigReader<Result<StoreProfileDto, CustomError>>

module StoreProfileUseCases =   

    let createProfile :CreateStoreProfileUseCase =            
        fun profileDto ->
            readerResult {
                let! _ = profileDto 
                        |> StoreProfileDto.toDomain 
                        |> CustomError.mapValidation 
                        |> ConfigReader.retn
                               
                let! profileId = profileDto |> StoreProfileRepo.createProfile

                let addressDto = {profileDto.Address with Id = ""}
                // Todo: why the next line never executes ShippingProfileRepo.createProfile
                //profileDto.Address |> ShippingProfileRepo.createProfile |> ignore
                let! _ = addressDto |> ShippingProfileRepo.createDefault

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

type CreateShippingProfileUseCase = ShippingProfileDto -> ConfigReader<Result<ShippingProfileDto, CustomError>>
type GetDefaultProfileUseCase = unit -> ConfigReader<Result<ShippingProfileDto, CustomError>>

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

                let! profileId = ShippingProfileRepo.createCustom dto
                return! ShippingProfileRepo.getProfile profileId
            }

type CreateShippingOriginUseCase = AddressDto -> ConfigReader<Result<ShippingOriginDto, CustomError>>
type GetShippingOriginsUseCase = string seq -> ConfigReader<Result<ShippingOriginDto seq, CustomError>>

module ShippingOriginUseCases =

    let createOrigin :CreateShippingOriginUseCase = 
        fun addressDto ->
            readerResult {
                let! originId = addressDto |> ShippingOriginRepo.createOrigin
                return! ShippingOriginRepo.getOrigin originId
            }

    let getOrigins :GetShippingOriginsUseCase = 
        fun ids ->
            readerResult {
                return! ShippingOriginRepo.getOrigins ids
            }


type CreatePaymentMethodUseCase = PaymentMethodDto -> ConfigReader<Result<PaymentMethodDto, CustomError>>

module PaymentMethodUseCases =

    let createMethod :CreatePaymentMethodUseCase = 
        fun methodDto ->
            readerResult {
                let! methodId = methodDto |> PaymentMethodRepo.createMethod
                return! PaymentMethodRepo.getMethod methodId
            }