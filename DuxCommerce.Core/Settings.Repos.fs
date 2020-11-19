namespace DuxCommerce.Settings

open System.Data.SqlClient
open DuxCommerce.Core.Common
open DuxCommerce.Settings.PublicTypes
open RepoDb
open System.Linq
open DuxCommerce.Settings.Ports

module StoreDetailsRepo =
    
    let createStoreDetails :CreateStoreDetails =
        fun storeDetailsDto ->
            let create (connection:SqlConnection) = 
                connection.Insert<StoreDetailsDto, int64>(storeDetailsDto) |> ignore
                storeDetailsDto.Id
                
            RepoAdapter.repoAdapter create
            
    let getStoreDetails :GetStoreDetails =
        fun id ->
            let get (connection:SqlConnection) =
                connection.Query<StoreDetailsDto>(fun p -> p.Id = id).FirstOrDefault()
                
            RepoAdapter.repoAdapter get          
            
    let updateStoreDetails :UpdateStoreDetails =
        fun id storeDetailsDto ->
            let update (connection:SqlConnection) =
                connection.Update<StoreDetailsDto>(storeDetailsDto, id) |> ignore
                
            RepoAdapter.repoAdapter update