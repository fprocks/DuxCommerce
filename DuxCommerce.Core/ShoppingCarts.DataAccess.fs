namespace DuxCommerce.ShoppingCarts

open System
open System.Data.SqlClient
open RepoDb

module DataAccess =
    
    let addCartItem connString cart cartItem :Result<CartItemInfo, string> =        
        try
            ( use connection = new SqlConnection(connString)
              connection.EnsureOpen() |> ignore
              ( use trans = connection.BeginTransaction()
                connection.Update<CartInfo>(cart, id) |> ignore
                connection.Insert<CartItemInfo, int64> (cartItem, transaction = trans) |> ignore
                trans.Commit() )
            )
            Ok cartItem
        with
            | :? Exception as ex -> Error ex.Message
