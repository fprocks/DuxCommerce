namespace DuxCommerce.Settings

open System.Data.SqlClient
open DuxCommerce.Core.Common
open DuxCommerce.Settings.PublicTypes
open RepoDb
open System.Linq
open DuxCommerce.Settings.Ports

module StoreDetailsRepo =
    
    let createStoreDetails :CreateStoreDetails =
        fun dto ->
            let create (connection:SqlConnection) =
                let addressId = connection.Insert<AddressDto, int64>(dto.Address)
                let dto = {dto with AddressId = addressId}
                connection.Insert<StoreDetailsDto, int64>(dto) |> ignore
                dto.Id
                
            RepoAdapter.repoAdapter create
            
    let getStoreDetails :GetStoreDetails =
        fun id ->
            let get (connection:SqlConnection) =
                let storeDto = connection.Query<StoreDetailsDto>(fun p -> p.Id = id).FirstOrDefault()
                let address = connection.Query<AddressDto>(fun (a:AddressDto) -> a.Id = storeDto.AddressId).FirstOrDefault()
                {storeDto with Address = address}
                
            RepoAdapter.repoAdapter get          
            
    let updateStoreDetails :UpdateStoreDetails =
        fun id dto ->
            let update (connection:SqlConnection) =
                connection.Update<StoreDetailsDto>(dto, id) |> ignore
                
            RepoAdapter.repoAdapter update