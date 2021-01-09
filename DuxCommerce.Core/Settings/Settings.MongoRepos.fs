namespace DuxCommerce.Settings.MongoRepos

open DuxCommerce.Core.Common
open DuxCommerce.Settings.PublicTypes
open DuxCommerce.Settings.Ports
open MongoDB.Driver

module StoreProfileRepo =
    
    let createProfile :CreateStoreProfile =
        fun profileDto ->
            let create (db:IMongoDatabase) =
                let addresses = db.GetCollection<AddressDto>(CollectionName.Address)
                addresses.InsertOne(profileDto.Address)

                let profiles = db.GetCollection<StoreProfileDto>(CollectionName.StoreProfile)
                let profileDto = {profileDto with AddressId = profileDto.Address.Id}
                profiles.InsertOne(profileDto)      
                
                profileDto.Id
                
            MongoRepoAdapter.repoAdapter create
            
    let getProfile :GetStoreProfile =
        fun id ->
            let get (db:IMongoDatabase) =
                let profiles = db.GetCollection<StoreProfileDto>(CollectionName.StoreProfile)
                let profileDto = profiles.Find(fun p -> p.Id = id).FirstOrDefault()

                let addresses = db.GetCollection<AddressDto>(CollectionName.Address)
                let address = addresses.Find(fun a -> a.Id = profileDto.AddressId).FirstOrDefault()

                {profileDto with Address = address}
                
            MongoRepoAdapter.repoAdapter get          
            
    let updateProfile :UpdateStoreProfile =
        fun id profileDto ->
            let update (db:IMongoDatabase) =
                let profiles = db.GetCollection<StoreProfileDto>(CollectionName.StoreProfile)
                let profileDto = {profileDto with Id = id}
                profiles.ReplaceOne((fun x -> x.Id = id), profileDto) |> ignore

                let addresses = db.GetCollection<AddressDto>(CollectionName.Address)
                let addressDto = {profileDto.Address with Id = profileDto.AddressId}
                addresses.ReplaceOne((fun x -> x.Id = profileDto.AddressId), addressDto) |> ignore
                
            MongoRepoAdapter.repoAdapter update

module ShippingOriginRepo =
    
    let createOrigin :CreateShippingOrigin =
        fun addressDto -> 
            let create (db:IMongoDatabase) =
                let addresses = db.GetCollection<AddressDto>(CollectionName.Address)
                addresses.InsertOne(addressDto)

                let profiles = db.GetCollection<ShippingOriginDto>(CollectionName.ShippingOrigin)
                let originDto = {Id = ""; Name = addressDto.AddressLine1; AddressId = addressDto.Id; IsDefault = true}
                profiles.InsertOne(originDto)

                originDto.Id

            MongoRepoAdapter.repoAdapter create

    let getOrigin :GetShippingOrigin =
        fun id ->
            let get (db:IMongoDatabase) =
                let profiles = db.GetCollection<ShippingOriginDto>(CollectionName.ShippingOrigin)
                profiles.Find(fun x -> x.Id = id).FirstOrDefault()

            MongoRepoAdapter.repoAdapter get          
        
    let updateOrigin :UpdateShippingOrigin =
        fun id originDto ->
            let update (db:IMongoDatabase) =
                let profiles = db.GetCollection<ShippingOriginDto>(CollectionName.ShippingOrigin)
                profiles.ReplaceOne((fun x -> x.Id = id), originDto) |> ignore
            
            MongoRepoAdapter.repoAdapter update

module ShippingProfileRepo = 

    let internal createProfile =
        fun (originId:string) (addressDto:AddressDto) ->
            let shippingCountry = {CountryCode = addressDto.CountryCode; StateNames = seq {addressDto.StateName}}

            let zoneDto = {
                Name = addressDto.CountryCode; 
                Methods = Seq.empty; 
                Countries = seq {shippingCountry}
                }

            let profileDto = {
                Id = "" 
                Name = "Default Profile"; 
                IsDefault = true; 
                OriginIds = seq {originId}; 
                Zones = seq {zoneDto}
                }

            profileDto


    let createDefault:CreateDefaultProfile =
        fun addressDto -> 
            let create (db:IMongoDatabase) =
                let addresses = db.GetCollection<AddressDto>(CollectionName.Address)
                addresses.InsertOne(addressDto)

                let originDto = {Id = ""; Name = addressDto.AddressLine1; AddressId = addressDto.Id; IsDefault = true}
                let origins = db.GetCollection<ShippingOriginDto>(CollectionName.ShippingOrigin)
                origins.InsertOne(originDto)

                let profileDto = createProfile originDto.Id addressDto
                let profiles = db.GetCollection<ShippingProfileDto>(CollectionName.ShippingProfile)
                profiles.InsertOne(profileDto)

                profileDto.Id

            MongoRepoAdapter.repoAdapter create

    let createCustom :CreateCustomProfile =
        fun profileDto -> 
            let create (db:IMongoDatabase) =
                let profiles = db.GetCollection<ShippingProfileDto>(CollectionName.ShippingProfile)
                profiles.InsertOne(profileDto)

                profileDto.Id

            MongoRepoAdapter.repoAdapter create

    let getDefault :GetDefaultProfile =
        fun () ->
            let get (db:IMongoDatabase) =
                let profiles = db.GetCollection<ShippingProfileDto>(CollectionName.ShippingProfile)
                profiles.Find(fun x -> x.IsDefault = true).FirstOrDefault()

            MongoRepoAdapter.repoAdapter get       

    let getProfile :GetShippingProfile=
        fun id ->
            let get (db:IMongoDatabase) =
                let profiles = db.GetCollection<ShippingProfileDto>(CollectionName.ShippingProfile)
                profiles.Find(fun x -> x.Id = id).FirstOrDefault()
                
            MongoRepoAdapter.repoAdapter get     
