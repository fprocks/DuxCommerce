namespace DuxCommerce.ShoppingCarts

open System.Data.SqlClient
open DuxCommerce.Common
open DuxCommerce.ShoppingCarts.Ports
open RepoDb
open System.Linq
open DuxCommerce.ShoppingCarts.PublicTypes

module CartRepo =
    
    let internal insertOrUpdate (connection:SqlConnection) (trans:SqlTransaction) (itemInfo:CartItemInfo) =
        if itemInfo.Id = 0L 
        then connection.Insert<CartItemInfo, int64>(itemInfo, transaction = trans) |> ignore
        else connection.Update<CartItemInfo>(itemInfo, itemInfo.Id, transaction = trans) |>ignore
            
    let saveShoppingCart connString :SaveShoppingCart =
        fun cartInfo ->       
            let save cartInfo =
                ( use connection = new SqlConnection(connString)
                  connection.EnsureOpen() |> ignore
                  ( use trans = connection.BeginTransaction()
                    connection.Update<CartInfo>(cartInfo, cartInfo.Id, transaction = trans) |> ignore                
                    // Todo: why will LineItems not be saved when changing Seq.iter to Seq.map
                    cartInfo.LineItems |> Seq.iter (insertOrUpdate connection trans) |> ignore
                    trans.Commit() )
                )
                
            RepoAdapter.repoAdapter1 save cartInfo
            
    let getShoppingCart connString :GetShoppingCart = 
        fun shopperId ->
            let get shopperId =
                ( use connection = new SqlConnection(connString)
                  connection.EnsureOpen() |> ignore
                  let cartInfo = connection.Query<CartInfo>(fun c -> c.ShopperId = shopperId).FirstOrDefault()
                  match box cartInfo with
                  | null -> 
                      let newCart: CartInfo = {Id = 0L; ShopperId = shopperId; LineItems = []; CartTotal = 0.0M}
                      connection.Insert<CartInfo, int64>(newCart) |> ignore
                      newCart
                  | _ -> 
                      let items = connection.Query<CartItemInfo>(fun c -> c.CartId = cartInfo.Id)
                      { cartInfo with LineItems = items}
                )
                
            RepoAdapter.repoAdapter1 get shopperId
    
    let deleteCartItem connString =
        fun (cartToUpdate, itemsToDelete: CartItemInfo seq) ->        
            let delete (cartToUpdate, itemsToDelete : CartItemInfo seq) =
                ( use connection = new SqlConnection(connString)
                  connection.EnsureOpen() |> ignore
                  ( use trans = connection.BeginTransaction()
                    connection.Update<CartInfo>(cartToUpdate, cartToUpdate.Id, transaction = trans) |> ignore
                                
                    itemsToDelete
                    |> Seq.iter (fun item -> connection.Delete<CartItemInfo>(item.Id, transaction = trans) |> ignore)
                    |> ignore
                
                    trans.Commit() )
                )
                
            RepoAdapter.repoAdapter1 delete (cartToUpdate, itemsToDelete)