namespace DuxCommerce.Core.Taxes.MongoRepos

open MongoDB.Driver
open DuxCommerce.Common
open DuxCommerce.Core.Taxes.PublicTypes

module TaxRateRepo =

    type CreateTaxRate = TaxRateDto -> ConfigReader<Result<string, CustomError>>    
    let createRate :CreateTaxRate =
        fun rateDto -> 
            let create (db:IMongoDatabase) =
                let rates = db.GetCollection<TaxRateDto>(CollectionName.TaxRate)
                rates.InsertOne(rateDto)

                rateDto.Id

            MongoRepoAdapter.repoAdapter create

    type GetTaxRate = string -> ConfigReader<Result<TaxRateDto, CustomError>>
    let getRate :GetTaxRate=
        fun id ->
            let get (db:IMongoDatabase) =
                let rates = db.GetCollection<TaxRateDto>(CollectionName.TaxRate)
                rates.Find(fun x -> x.Id = id).FirstOrDefault()
            
            MongoRepoAdapter.repoAdapter get
