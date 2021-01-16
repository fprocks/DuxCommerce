namespace DuxCommerce.Core.Shipping.MongoRepos

open DuxCommerce.Core.Shared
open DuxCommerce.Core.Shipping.PublicTypes
open MongoDB.Driver
open DuxCommerce.Common

module StoreProfileRepo =

    type CreateStoreProfile = StoreProfileDto -> ConfigReader<Result<string , CustomError>>
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
            
    type GetStoreProfile = string -> ConfigReader<Result<StoreProfileDto, CustomError>>
    let getProfile :GetStoreProfile =
        fun id ->
            let get (db:IMongoDatabase) =
                let profiles = db.GetCollection<StoreProfileDto>(CollectionName.StoreProfile)
                let profileDto = profiles.Find(fun p -> p.Id = id).FirstOrDefault()

                let addresses = db.GetCollection<AddressDto>(CollectionName.Address)
                let address = addresses.Find(fun a -> a.Id = profileDto.AddressId).FirstOrDefault()

                {profileDto with Address = address}
                
            MongoRepoAdapter.repoAdapter get          
            
    type UpdateStoreProfile = string -> StoreProfileDto -> ConfigReader<Result<unit, CustomError>>    
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

    type CreateShippingOrigin = AddressDto -> ConfigReader<Result<string, CustomError>>
    let createOrigin :CreateShippingOrigin =
        fun addressDto -> 
            let create (db:IMongoDatabase) =
                let addresses = db.GetCollection<AddressDto>(CollectionName.Address)
                addresses.InsertOne(addressDto)

                let profiles = db.GetCollection<ShippingOriginDto>(CollectionName.ShippingOrigin)
                let originDto = {Id = ""; Name = addressDto.AddressLine1; Address = addressDto; IsDefault = true}
                profiles.InsertOne(originDto)

                originDto.Id

            MongoRepoAdapter.repoAdapter create

    type GetShippingOrigins = string seq -> ConfigReader<Result<ShippingOriginDto seq, CustomError>>
    let getOrigins :GetShippingOrigins =
        fun ids ->
            let get (db:IMongoDatabase) =
                let origins = db.GetCollection<ShippingOriginDto>(CollectionName.ShippingOrigin)
                let filter = Builders<ShippingOriginDto>.Filter.In((fun x -> x.Id), ids)
                origins.Find(filter).ToEnumerable()

                // Todo: the following filter is not supported
                //origins.Find(fun x -> Seq.exists ((=) x.Id) ids).ToEnumerable()

            MongoRepoAdapter.repoAdapter get
            
    type GetShippingOrigin = string -> ConfigReader<Result<ShippingOriginDto, CustomError>>
    let getOrigin :GetShippingOrigin =
        fun id ->
            let get (db:IMongoDatabase) =
                let origins = db.GetCollection<ShippingOriginDto>(CollectionName.ShippingOrigin)
                origins.Find(fun x -> x.Id = id).FirstOrDefault()

            MongoRepoAdapter.repoAdapter get
        
    type UpdateShippingOrigin = string -> ShippingOriginDto -> ConfigReader<Result<unit, CustomError>>
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
            
    type CreateDefaultProfile = AddressDto -> ConfigReader<Result<string, CustomError>>
    let createDefault:CreateDefaultProfile =
        fun addressDto -> 
            let create (db:IMongoDatabase) =
                let addresses = db.GetCollection<AddressDto>(CollectionName.Address)
                addresses.InsertOne(addressDto)

                let originDto = {Id = ""; Name = addressDto.AddressLine1; Address = addressDto; IsDefault = true}
                let origins = db.GetCollection<ShippingOriginDto>(CollectionName.ShippingOrigin)
                origins.InsertOne(originDto)

                let profileDto = createProfile originDto.Id addressDto
                let profiles = db.GetCollection<ShippingProfileDto>(CollectionName.ShippingProfile)
                profiles.InsertOne(profileDto)

                profileDto.Id

            MongoRepoAdapter.repoAdapter create

    type CreateCustomProfile = ShippingProfileDto -> ConfigReader<Result<string, CustomError>>
    let createCustom :CreateCustomProfile =
        fun profileDto -> 
            let create (db:IMongoDatabase) =
                let profiles = db.GetCollection<ShippingProfileDto>(CollectionName.ShippingProfile)
                profiles.InsertOne(profileDto)

                profileDto.Id

            MongoRepoAdapter.repoAdapter create

    type GetDefaultProfile = unit -> ConfigReader<Result<ShippingProfileDto, CustomError>>
    let getDefault :GetDefaultProfile =
        fun () ->
            let get (db:IMongoDatabase) =
                let profiles = db.GetCollection<ShippingProfileDto>(CollectionName.ShippingProfile)
                profiles.Find(fun x -> x.IsDefault = true).FirstOrDefault()

            MongoRepoAdapter.repoAdapter get       
            
    type GetShippingProfile = string -> ConfigReader<Result<ShippingProfileDto, CustomError>>
    let getProfile :GetShippingProfile=
        fun id ->
            let get (db:IMongoDatabase) =
                let profiles = db.GetCollection<ShippingProfileDto>(CollectionName.ShippingProfile)
                profiles.Find(fun x -> x.Id = id).FirstOrDefault()
                
            MongoRepoAdapter.repoAdapter get