namespace DuxCommerce.Settings.Ports

open DuxCommerce.Common
open DuxCommerce.Settings.PublicTypes

// Input port
type CreateStoreDetailsUseCase = StoreDetailsDto -> ConfigReader<Result<StoreDetailsDto, CustomError>>
type GetStoreDetailsUseCase = int64 -> ConfigReader<Result<StoreDetailsDto, CustomError>>
type UpdateStoreDetailsUseCase = int64 -> StoreDetailsDto -> ConfigReader<Result<StoreDetailsDto, CustomError>>


// Output port
type CreateStoreDetails = StoreDetailsDto -> ConfigReader<Result<int64, CustomError>>
type GetStoreDetails = int64 -> ConfigReader<Result<StoreDetailsDto, CustomError>>
type UpdateStoreDetails = int64 -> StoreDetailsDto -> ConfigReader<Result<unit, CustomError>>

type CreateWarehouse = WarehouseDto -> ConfigReader<Result<int64, CustomError>>
