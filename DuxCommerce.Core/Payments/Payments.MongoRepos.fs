namespace DuxCommerce.Core.Payments.MongoRepos

open DuxCommerce.Core.Payments.PublicTypes
open MongoDB.Driver
open DuxCommerce.Common

module PaymentMethodRepo =

    type CreatePaymentMethod = PaymentMethodDto -> ConfigReader<Result<string, CustomError>>

    let createMethod: CreatePaymentMethod =
        fun methodDto ->
            let create (db: IMongoDatabase) =
                let methods =
                    db.GetCollection<PaymentMethodDto>(CollectionName.PaymentMethod)

                methods.InsertOne(methodDto)

                methodDto.Id

            MongoRepoAdapter.repoAdapter create

    type GetPaymentMethod = string -> ConfigReader<Result<PaymentMethodDto, CustomError>>

    let getMethod: GetPaymentMethod =
        fun id ->
            let get (db: IMongoDatabase) =
                let methods =
                    db.GetCollection<PaymentMethodDto>(CollectionName.PaymentMethod)

                methods.Find(fun x -> x.Id = id).FirstOrDefault()

            MongoRepoAdapter.repoAdapter get
