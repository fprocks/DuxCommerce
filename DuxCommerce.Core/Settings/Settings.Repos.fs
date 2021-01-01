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
                let originDto = {Id = 0L; Name = addressDto.AddressLine1; AddressId = addressId; IsDefault = true; Address = Unchecked.defaultof<AddressDto>}
                let originId = connection.Insert<ShippingOriginDto, int64>(originDto)
                originId

            RepoAdapter.repoAdapter create

    let getOrigin :GetShippingOrigin =
        fun id ->
            let get (connection:SqlConnection) =
                connection.Query<ShippingOriginDto>(fun (p:ShippingOriginDto) -> p.Id = id).FirstOrDefault()
            
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

                // Todo: performance tuning
                let profileDto = {Id = 0L; Name = "Default Profile"; IsDefault = true; OriginIds = Seq.empty; Zones = Seq.empty}
                let profileId = connection.Insert<ShippingProfileDto, int64>(profileDto)

                let profileOrigin = {Id = 0L; ShippingProfileId = profileId; ShippingOriginId = originId}
                connection.Insert<ShippingProfileOriginDto, int64>(profileOrigin) |> ignore

                let zoneDto = {Id = 0L; Name = addressDto.CountryCode; ShippingProfileId = profileId; Methods = Seq.empty; Countries = Seq.empty}
                let zoneId = connection.Insert<ShippingZoneDto, int64>(zoneDto)

                let shippingCountry = {Id = 0L; ShippingZoneId = zoneId; CountryCode = addressDto.CountryCode; StateIds = Seq.empty}
                let shippingCountryId = connection.Insert<ShippingCountryDto, int64>(shippingCountry)

                connection.Query<StateDto>(fun (s:StateDto) -> s.CountryCode = addressDto.CountryCode)
                |> Seq.map (fun s -> {Id = 0L; ShippingCountryId = shippingCountryId; StateId = s.Id})
                |> Seq.iter (fun s -> connection.Insert<ShippingStateDto, int64>(s) |> ignore )

                profileId

            RepoAdapter.repoAdapter create

    let getDefaultProfile :GetDefaultProfile=
        fun () ->
            let get (connection:SqlConnection) =

                // Todo: performance tuning
                //let profile = connection.Query<ShippingProfileDto>(fun p -> p.IsDefault = true).FirstOrDefault()
                let profile = connection.Query<ShippingProfileDto>(fun p -> p.Name = "Default Profile").FirstOrDefault()

                let originIds = connection.Query<ShippingProfileOriginDto>(fun (o:ShippingProfileOriginDto) -> o.ShippingProfileId = profile.Id)
                                |> Seq.map (fun o -> o.ShippingOriginId)

                let shippingZones = connection.Query<ShippingZoneDto>(fun z -> z.ShippingProfileId = profile.Id)

                let zoneIds = shippingZones |> Seq.map(fun z -> z.Id)
                let shippingCountries = connection.Query<ShippingCountryDto>(fun (c:ShippingCountryDto) -> zoneIds.Contains(c.ShippingZoneId))

                let shippingCountryIds = shippingCountries |> Seq.map (fun c -> c.Id) 
                let shippingStates = connection.Query<ShippingStateDto>(fun s -> shippingCountryIds.Contains(s.ShippingCountryId))                               

                let filterStates shippingStates shippingCountryId = 
                    shippingStates 
                    |> Seq.filter(fun s -> s.ShippingCountryId = shippingCountryId)
                    |> Seq.map(fun s -> s.StateId)

                let shippingCountries = shippingCountries 
                                        |> Seq.map (fun c -> {c with StateIds = (filterStates shippingStates c.Id)})

                let filterCountries (countries:ShippingCountryDto seq) zoneId =
                    countries |> Seq.filter(fun c -> c.ShippingZoneId = zoneId)

                let shippingZones = shippingZones
                                    |> Seq.map(fun z -> {z with Countries = (filterCountries shippingCountries z.Id)})

                {profile with OriginIds = originIds; Zones = shippingZones}

            RepoAdapter.repoAdapter get       
