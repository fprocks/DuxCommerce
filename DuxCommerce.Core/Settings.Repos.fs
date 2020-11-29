namespace DuxCommerce.Settings

open System.Data.SqlClient
open DuxCommerce.Core.Common
open DuxCommerce.Settings.PublicTypes
open RepoDb
open System.Linq
open DuxCommerce.Settings.Ports

module StoreDetailsRepo =
    
    let createStoreDetails :CreateStoreDetails =
        fun storeDto ->
            let create (connection:SqlConnection) =
                let addressId = connection.Insert<AddressDto, int64>(storeDto.Address)
                let dto = {storeDto with AddressId = addressId}
                connection.Insert<StoreDetailsDto, int64>(dto) |> ignore               
                dto.Id
                
            RepoAdapter.repoAdapter create
            
    let getStoreDetails :GetStoreDetails =
        fun id ->
            let get (connection:SqlConnection) =
                let storeDto = connection.Query<StoreDetailsDto>(fun (p:StoreDetailsDto) -> p.Id = id).FirstOrDefault()
                let address = connection.Query<AddressDto>(fun (a:AddressDto) -> a.Id = storeDto.AddressId).FirstOrDefault()
                {storeDto with Address = address}
                
            RepoAdapter.repoAdapter get          
            
    let updateStoreDetails :UpdateStoreDetails =
        fun id storeDto ->
            let update (connection:SqlConnection) =
                connection.Update<StoreDetailsDto>(storeDto, id) |> ignore
                connection.Update<AddressDto>(storeDto.Address, storeDto.AddressId) |> ignore
                
            RepoAdapter.repoAdapter update

module WarehouseRepo =
    
    let createWarehouse :CreateWarehouse =
        fun addressDto -> 
            let create (connection:SqlConnection) =
                let addressId = connection.Insert<AddressDto, int64>(addressDto)

                let warehouse = {Id = 0L; Name = addressDto.AddressLine1; AddressId = addressId; IsDefault = true}
                connection.Insert<WarehouseDto, int64>(warehouse) |> ignore
                warehouse.Id

            RepoAdapter.repoAdapter create

    let getWarehouse :GetWarehouse =
        fun id ->
            let get (connection:SqlConnection) =
                connection.Query<WarehouseDto>(fun p -> p.Id = id).FirstOrDefault()
            
            RepoAdapter.repoAdapter get          
        
    let updateProduct :UpdateWarehouse =
        fun id warehouseDto ->
            let update (connection:SqlConnection) =
                connection.Update<WarehouseDto>(warehouseDto, id) |> ignore
            
            RepoAdapter.repoAdapter update
