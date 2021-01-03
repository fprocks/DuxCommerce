namespace DuxCommerce.Settings.SqlRepos

open System.Data.SqlClient
open DuxCommerce.Core.Common
open DuxCommerce.Settings.PublicTypes
open RepoDb
open System.Linq
open DuxCommerce.Settings.Ports

module StoreProfileRepo =
    
    let createProfile :CreateStoreProfile =
        fun profileDto ->
            let create (connection:SqlConnection) =
                let addressId = connection.Insert<AddressDto, int64>(profileDto.Address)
                let profileDto = {profileDto with AddressId = addressId}
                let profileId = connection.Insert<StoreProfileDto, int64>(profileDto)               
                profileId
                
            RepoAdapter.repoAdapter create
            
    let getProfile :GetStoreProfile =
        fun id ->
            let get (connection:SqlConnection) =
                let profileDto = connection.Query<StoreProfileDto>(fun (p:StoreProfileDto) -> p.Id = id).FirstOrDefault()
                let address = connection.Query<AddressDto>(fun (a:AddressDto) -> a.Id = profileDto.AddressId).FirstOrDefault()
                {profileDto with Address = address}
                
            RepoAdapter.repoAdapter get          
            
    let updateProfile :UpdateStoreProfile =
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

    let createDefaultProfile :CreateDefaultProfile =
        fun addressDto -> 
            let create (connection:SqlConnection) =
                let addressId = connection.Insert<AddressDto, int64>(addressDto)
                let originDto = {Id = 0L; Name = addressDto.AddressLine1; AddressId = addressId; IsDefault = true; Address = Unchecked.defaultof<AddressDto>}
                let originId = connection.Insert<ShippingOriginDto, int64>(originDto)

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

    let createCustomProfile :CreateCustomProfile =
        fun profileDto -> 
            let create (connection:SqlConnection) =
                let profileId = connection.Insert<ShippingProfileDto, int64>(profileDto)

                let createOrigin originId = 
                    {Id = 0L; ShippingProfileId = profileId; ShippingOriginId = originId}

                profileDto.OriginIds
                |> Seq.map createOrigin
                |> Seq.iter (fun o -> connection.Insert<ShippingProfileOriginDto, int64>(o) |> ignore)

                let createState countryId stateId =
                    let state = {Id = 0L; ShippingCountryId = countryId; StateId = stateId}
                    connection.Insert<ShippingStateDto, int64>(state)

                let createCountry zoneId (countryDto:ShippingCountryDto) =
                    let countryDto = {countryDto with ShippingZoneId = zoneId}
                    let countryId = connection.Insert<ShippingCountryDto, int64>(countryDto)
                    countryDto.StateIds
                    |> Seq.iter (fun id -> (createState countryId id) |> ignore)
                
                let createRate methodId (rateDto:ShippingRateDto) =
                    let rateDto = {rateDto with ShippingMethodId = methodId}
                    connection.Insert<ShippingRateDto, int64>(rateDto)

                let createMethod zoneId (methodDto:ShippingMethodDto) =
                    let methodDto = {methodDto with ShippingZoneId = zoneId}
                    let methodId = connection.Insert<ShippingMethodDto, int64>(methodDto)
                    methodDto.Rates
                    |> Seq.iter (fun r -> (createRate methodId r) |> ignore)

                let createZone (zoneDto:ShippingZoneDto) =
                    let zoneDto = {zoneDto with ShippingProfileId = profileId}
                    let zoneId = connection.Insert<ShippingZoneDto, int64>(zoneDto)
                    zoneDto.Countries
                    |> Seq.iter (fun c -> (createCountry zoneId c) |> ignore)
                    zoneDto.Methods
                    |> Seq.iter (fun m -> (createMethod zoneId m) |> ignore)

                profileDto.Zones
                |> Seq.iter (fun z -> (createZone z) |> ignore)

                profileId

            RepoAdapter.repoAdapter create

    let internal getProfileDetails =
        fun (connection:SqlConnection) profile ->
            // Shipping zones
            let shippingZones = connection.Query<ShippingZoneDto>(fun z -> z.ShippingProfileId = profile.Id)
            let zoneIds = shippingZones |> Seq.map(fun z -> z.Id)

            // Shipping methods and rates
            let shippingMethods = connection.Query<ShippingMethodDto>(fun (m:ShippingMethodDto) -> zoneIds.Contains(m.ShippingZoneId))
            let shippingMethodIds = shippingMethods |> Seq.map (fun m -> m.Id)
            let shippingRates = connection.Query<ShippingRateDto>(fun r -> shippingMethodIds.Contains(r.ShippingMethodId))

            let filterRates shippingRates shippingMethodId =
                shippingRates
                |> Seq.filter (fun r -> r.ShippingMethodId = shippingMethodId)
            let shippingMethods = shippingMethods
                                  |> Seq.map (fun m -> {m with Rates = (filterRates shippingRates m.Id)})

            let filterMethods methods zoneId =
                methods |> Seq.filter (fun m -> m.ShippingZoneId = zoneId)
            let shippingZones = shippingZones
                                |> Seq.map(fun z -> {z with Methods = (filterMethods shippingMethods z.Id)})

            // Shipping countries and states
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
            
            // Shipping origins
            let originIds = connection.Query<ShippingProfileOriginDto>(fun (o:ShippingProfileOriginDto) -> o.ShippingProfileId = profile.Id)
                            |> Seq.map (fun o -> o.ShippingOriginId)

            {profile with OriginIds = originIds; Zones = shippingZones}

    let getDefault :GetDefaultProfile =
        fun () ->
            let get (connection:SqlConnection) =
                // connection.Query<ShippingProfileDto>(fun p -> p.IsDefault = true).FirstOrDefault()            
                connection.Query<ShippingProfileDto>(fun p -> p.Name = "Default Profile").FirstOrDefault()
                |> getProfileDetails connection

            RepoAdapter.repoAdapter get       

    let getProfile :GetShippingProfile=
        fun id ->
            let get (connection:SqlConnection) =
                connection.Query<ShippingProfileDto>(fun p -> p.Id = id).FirstOrDefault()
                |> getProfileDetails connection
                
            RepoAdapter.repoAdapter get     
