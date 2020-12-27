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
                let profileDto = {profileDto with AddressId = addressId}
                let profileId = connection.Insert<StoreProfileDto, int64>(profileDto)               
                profileId
                
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
                let originDto = {Id = 0L; Name = addressDto.AddressLine1; AddressId = addressId; IsDefault = true}
                let originId = connection.Insert<ShippingOriginDto, int64>(originDto)
                originId

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

module ShippingProfileRepo = 

    let createProfile :CreateShippingProfile =
        fun originId addressDto -> 
            let create (connection:SqlConnection) =
                let profileDto = {Id = 0L; Name = "Default Profile"; IsDefault = true}
                let profileId = connection.Insert<ShippingProfileDto, int64>(profileDto)

                let profileOrigin = {Id = 0L; ShippingProfileId = profileId; ShippingOriginId = originId}
                let _ = connection.Insert<ShippingProfileOriginDto, int64>(profileOrigin);

                let zoneDto = {Id = 0L; Name = addressDto.CountryCode; ShippingProfileId = profileId}
                let zoneId = connection.Insert<ShippingZoneDto, int64>(zoneDto)
                zoneId

            RepoAdapter.repoAdapter create
