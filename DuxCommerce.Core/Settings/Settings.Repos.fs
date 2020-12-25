namespace DuxCommerce.Settings

open System.Data.SqlClient
open DuxCommerce.Core.Common
open DuxCommerce.Settings.PublicTypes
open RepoDb
open System.Linq
open DuxCommerce.Settings.Ports

module StoreProfileRepo =
    
    let createStoreProfile :CreateStoreProfile =
        fun profileDto ->
            let create (connection:SqlConnection) =
                let addressId = connection.Insert<AddressDto, int64>(profileDto.Address)
                let dto = {profileDto with AddressId = addressId}
                connection.Insert<StoreProfileDto, int64>(dto) |> ignore               
                dto.Id
                
            RepoAdapter.repoAdapter create
            
    let getStoreProfile :GetStoreProfile =
        fun id ->
            let get (connection:SqlConnection) =
                let profileDto = connection.Query<StoreProfileDto>(fun (p:StoreProfileDto) -> p.Id = id).FirstOrDefault()
                let address = connection.Query<AddressDto>(fun (a:AddressDto) -> a.Id = profileDto.AddressId).FirstOrDefault()
                {profileDto with Address = address}
                
            RepoAdapter.repoAdapter get          
            
    let updateStoreProfile :UpdateStoreProfile =
        fun id profileDto ->
            let update (connection:SqlConnection) =
                connection.Update<StoreProfileDto>(profileDto, id) |> ignore
                connection.Update<AddressDto>(profileDto.Address, profileDto.AddressId) |> ignore
                
            RepoAdapter.repoAdapter update

module ShippingOriginRepo =
    
    let createOrigin :CreateShippingOrigin =
        fun addressDto -> 
            let create (connection:SqlConnection) =
                let addressId = connection.Insert<AddressDto, int64>(addressDto)

                let origin = {Id = 0L; Name = addressDto.AddressLine1; AddressId = addressId; IsDefault = true}
                connection.Insert<ShippingOriginDto, int64>(origin) |> ignore
                origin.Id

            RepoAdapter.repoAdapter create

    let getOrigin :GetShippingOrigin =
        fun id ->
            let get (connection:SqlConnection) =
                connection.Query<ShippingOriginDto>(fun p -> p.Id = id).FirstOrDefault()
            
            RepoAdapter.repoAdapter get          
        
    let updateOrigin :UpdateShippingOrigin =
        fun id originDto ->
            let update (connection:SqlConnection) =
                connection.Update<ShippingOriginDto>(originDto, id) |> ignore
            
            RepoAdapter.repoAdapter update
