namespace DuxCommerce.Core.ShoppingCarts.MongoRepos

open DuxCommerce.Core.Common
open DuxCommerce.Core.ShoppingCarts.PublicTypes
open MongoDB.Driver
open DuxCommerce.Common

type GetShoppingCart = string -> ConfigReader<Result<ShoppingCartDto, CustomError>>
type SaveShoppingCart = ShoppingCartDto -> ConfigReader<Result<unit, CustomError>>
type DeleteCartItem = ShoppingCartDto * (CartItemDto seq) -> ConfigReader<Result<uint, CustomError>>

module CartRepo =
                
    let saveShoppingCart :SaveShoppingCart =
        fun cartDto ->      

            let save (db:IMongoDatabase) =
                let shoppingCarts = db.GetCollection<ShoppingCartDto>(CollectionName.ShoppingCart)
                shoppingCarts.ReplaceOne((fun x -> x.Id = cartDto.Id),cartDto) |> ignore

            MongoRepoAdapter.repoAdapter save
            
    let getShoppingCart :GetShoppingCart = 
        fun shopperId ->
            
            let get (db:IMongoDatabase) =
                let shoppingCarts = db.GetCollection<ShoppingCartDto>(CollectionName.ShoppingCart)
                let cartDto = shoppingCarts.Find(fun c -> c.ShopperId = shopperId).FirstOrDefault()
                match box cartDto with
                | null -> 
                    let newCart: ShoppingCartDto = {Id = ""; ShopperId = shopperId; LineItems = []; CartTotal = 0.0M}
                    shoppingCarts.InsertOne(newCart) |> ignore
                    newCart
                | _ -> cartDto
                
            MongoRepoAdapter.repoAdapter get
    
    let deleteCartItem =
        fun (cartToUpdate, itemsToDelete: CartItemDto seq) ->
            
            let delete (db:IMongoDatabase) = failwith  ""
                
            MongoRepoAdapter.repoAdapter delete 