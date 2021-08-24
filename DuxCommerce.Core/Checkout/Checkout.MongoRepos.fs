namespace DuxCommerce.Core.Checkout.MongoRepos

open DuxCommerce.Common
open DuxCommerce.Core.Checkout.PublicTypes
open DuxCommerce.Core.Checkout.Dto
open MongoDB.Driver

module CheckoutRepo =

    type GetCheckout = string -> ConfigReader<Result<CheckoutDto, CustomError>>

    let getCheckout: GetCheckout =
        fun shopperId ->
            let get (db: IMongoDatabase) =
                let checkouts =
                    db.GetCollection<CheckoutDto>(CollectionName.Checkout)

                let checkoutDto =
                    checkouts
                        .Find(fun c -> c.ShopperId = shopperId)
                        .FirstOrDefault()

                match box checkoutDto with
                | null ->
                    let newCheckout =
                        { CheckoutDto.create with
                              ShopperId = shopperId }

                    checkouts.InsertOne(newCheckout) |> ignore
                    newCheckout
                | _ -> checkoutDto

            MongoRepoAdapter.repoAdapter get

    type SaveCheckout = CheckoutDto -> ConfigReader<Result<unit, CustomError>>

    let saveCheckout: SaveCheckout =
        fun dto ->
            let save (db: IMongoDatabase) =
                let checkouts =
                    db.GetCollection<CheckoutDto>(CollectionName.Checkout)

                checkouts.ReplaceOne((fun x -> x.Id = dto.Id), dto)
                |> ignore

            MongoRepoAdapter.repoAdapter save
