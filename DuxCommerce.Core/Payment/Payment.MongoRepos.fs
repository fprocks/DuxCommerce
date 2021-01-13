namespace DuxCommerce.Payment.MongoRepos

open DuxCommerce.Core.Common
open DuxCommerce.Payment.PublicTypes
open MongoDB.Driver
open DuxCommerce.Common

type CreatePaymentMethod = PaymentMethodDto -> ConfigReader<Result<string, CustomError>>
type GetPaymentMethod = string -> ConfigReader<Result<PaymentMethodDto, CustomError>>

module PaymentMethodRepo =
    
    let createMethod :CreatePaymentMethod =
        fun methodDto -> 
            let create (db:IMongoDatabase) =
                let methods = db.GetCollection<PaymentMethodDto>(CollectionName.PaymentMethod)
                methods.InsertOne(methodDto)

                methodDto.Id

            MongoRepoAdapter.repoAdapter create

    let getMethod :GetPaymentMethod=
        fun id ->
            let get (db:IMongoDatabase) =
                let methods = db.GetCollection<PaymentMethodDto>(CollectionName.PaymentMethod)
                methods.Find(fun x -> x.Id = id).FirstOrDefault()
            
            MongoRepoAdapter.repoAdapter get
