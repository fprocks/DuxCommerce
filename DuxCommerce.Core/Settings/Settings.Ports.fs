namespace DuxCommerce.Settings.Ports

open DuxCommerce.Common
open DuxCommerce.Settings.PublicTypes

// Input port
type CreateStoreProfileUseCase = StoreProfileDto -> ConfigReader<Result<StoreProfileDto, CustomError>>
type GetStoreProfileUseCase = int64 -> ConfigReader<Result<StoreProfileDto, CustomError>>
type UpdateStoreProfileUseCase = int64 -> StoreProfileDto -> ConfigReader<Result<StoreProfileDto, CustomError>>

// Output port
type CreateStoreProfile = StoreProfileDto -> ConfigReader<Result<int64, CustomError>>
type GetStoreProfile = int64 -> ConfigReader<Result<StoreProfileDto, CustomError>>
type UpdateStoreProfile = int64 -> StoreProfileDto -> ConfigReader<Result<unit, CustomError>>

type CreateShippingOrigin = AddressDto -> ConfigReader<Result<int64, CustomError>>
type GetShippingOrigin = int64 -> ConfigReader<Result<ShippingOriginDto, CustomError>>
type UpdateShippingOrigin = int64 -> ShippingOriginDto -> ConfigReader<Result<unit, CustomError>>
