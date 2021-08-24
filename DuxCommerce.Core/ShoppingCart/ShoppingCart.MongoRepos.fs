namespace DuxCommerce.Core.ShoppingCarts.MongoRepos

open DuxCommerce.Core.ShoppingCarts.PublicTypes
open MongoDB.Driver
open DuxCommerce.Common

module CartRepo =

    type SaveShoppingCart = ShoppingCartDto -> ConfigReader<Result<unit, CustomError>>

    let saveShoppingCart: SaveShoppingCart =
        fun cartDto ->

            let save (db: IMongoDatabase) =
                let shoppingCarts =
                    db.GetCollection<ShoppingCartDto>(CollectionName.ShoppingCart)

                shoppingCarts.ReplaceOne((fun x -> x.Id = cartDto.Id), cartDto)
                |> ignore

            MongoRepoAdapter.repoAdapter save

    type GetShoppingCart = string -> ConfigReader<Result<ShoppingCartDto, CustomError>>

    let getShoppingCart: GetShoppingCart =
        fun shopperId ->

            let get (db: IMongoDatabase) =
                let shoppingCarts =
                    db.GetCollection<ShoppingCartDto>(CollectionName.ShoppingCart)

                let cartDto =
                    shoppingCarts
                        .Find(fun c -> c.ShopperId = shopperId)
                        .FirstOrDefault()

                match box cartDto with
                | null ->
                    let newCart: ShoppingCartDto =
                        { Id = ""
                          ShopperId = shopperId
                          LineItems = []
                          CartTotal = 0.0M }

                    shoppingCarts.InsertOne(newCart) |> ignore
                    newCart
                | _ -> cartDto

            MongoRepoAdapter.repoAdapter get
