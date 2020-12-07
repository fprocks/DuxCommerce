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

type CreateLocation = AddressDto -> ConfigReader<Result<int64, CustomError>>
type GetLocation = int64 -> ConfigReader<Result<LocationDto, CustomError>>
type UpdateLocation = int64 -> LocationDto -> ConfigReader<Result<unit, CustomError>>
