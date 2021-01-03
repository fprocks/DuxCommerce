namespace DuxCommerce.Settings.Ports

open DuxCommerce.Common
open DuxCommerce.Settings.PublicTypes

// Input port

type CreateStoreProfileUseCase = StoreProfileDto -> ConfigReader<Result<StoreProfileDto, CustomError>>
type GetStoreProfileUseCase = string -> ConfigReader<Result<StoreProfileDto, CustomError>>
type UpdateStoreProfileUseCase = string -> StoreProfileDto -> ConfigReader<Result<StoreProfileDto, CustomError>>

type CreateShippingProfileUseCase = ShippingProfileDto -> ConfigReader<Result<ShippingProfileDto, CustomError>>
type GetDefaultProfileUseCase = unit -> ConfigReader<Result<ShippingProfileDto, CustomError>>

type CreateShippingOriginUseCase = AddressDto -> ConfigReader<Result<ShippingOriginDto, CustomError>>

// Output port
type CreateStoreProfile = StoreProfileDto -> ConfigReader<Result<string , CustomError>>
type GetStoreProfile = string -> ConfigReader<Result<StoreProfileDto, CustomError>>
type UpdateStoreProfile = string -> StoreProfileDto -> ConfigReader<Result<unit, CustomError>>

type CreateShippingOrigin = AddressDto -> ConfigReader<Result<string, CustomError>>
type GetShippingOrigin = string -> ConfigReader<Result<ShippingOriginDto, CustomError>>
type UpdateShippingOrigin = string -> ShippingOriginDto -> ConfigReader<Result<unit, CustomError>>

type CreateDefaultProfile = AddressDto -> ConfigReader<Result<string, CustomError>>
type CreateCustomProfile = ShippingProfileDto -> ConfigReader<Result<string, CustomError>>
type GetDefaultProfile = unit -> ConfigReader<Result<ShippingProfileDto, CustomError>>
type GetShippingProfile = string -> ConfigReader<Result<ShippingProfileDto, CustomError>>
