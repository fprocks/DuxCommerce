namespace DuxCommerce.ShoppingCarts

open System.Data.SqlClient
open DuxCommerce.Core.Common
open DuxCommerce.ShoppingCarts.Ports
open RepoDb
open System.Linq
open DuxCommerce.ShoppingCarts.PublicTypes

module CartRepo =
                
    let saveShoppingCart connString :SaveShoppingCart =
        fun cartInfo ->      

            let insertOrUpdate (connection:SqlConnection) (itemInfo:CartItemInfo) =
                if itemInfo.Id = 0L 
                then connection.Insert<CartItemInfo, int64>(itemInfo) |> ignore
                else connection.Update<CartItemInfo>(itemInfo, itemInfo.Id) |>ignore
                
            let save (connection:SqlConnection) cartInfo =
                connection.Update<CartInfo>(cartInfo, cartInfo.Id) |> ignore

                cartInfo.LineItems 
                |> Seq.iter (insertOrUpdate connection) 
                |> ignore

            RepoAdapter.repoAdapter1 connString save cartInfo
            
    let getShoppingCart connString :GetShoppingCart = 
        fun shopperId ->
            let get (connection:SqlConnection) shopperId =
                let cartInfo = connection.Query<CartInfo>(fun c -> c.ShopperId = shopperId).FirstOrDefault()
                match box cartInfo with
                | null -> 
                    let newCart: CartInfo = {Id = 0L; ShopperId = shopperId; LineItems = []; CartTotal = 0.0M}
                    connection.Insert<CartInfo, int64>(newCart) |> ignore
                    newCart
                | _ -> 
                    let items = connection.Query<CartItemInfo>(fun c -> c.CartId = cartInfo.Id)
                    { cartInfo with LineItems = items}
                
            RepoAdapter.repoAdapter1 connString get shopperId
    
    let deleteCartItem connString =
        fun (cartToUpdate, itemsToDelete: CartItemInfo seq) ->        
            let delete (connection:SqlConnection) (cartToUpdate, itemsToDelete : CartItemInfo seq) =
                ( use trans = connection.BeginTransaction()
                  connection.Update<CartInfo>(cartToUpdate, cartToUpdate.Id, transaction = trans) |> ignore
                                  
                  itemsToDelete
                  |> Seq.iter (fun item -> connection.Delete<CartItemInfo>(item.Id, transaction = trans) |> ignore)
                  |> ignore
                  
                  trans.Commit() )
                
            RepoAdapter.repoAdapter1 connString delete (cartToUpdate, itemsToDelete)