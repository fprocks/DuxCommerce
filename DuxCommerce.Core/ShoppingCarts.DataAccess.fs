namespace DuxCommerce.ShoppingCarts

open System
open System.Data.SqlClient
open RepoDb

module ShoppingCartDb =
    
    let internal insertLineItem (connection:SqlConnection) (cartItem:CartItemInfo) =
        if cartItem.Id = 0L then
            connection.Insert<CartItemInfo, int64>(cartItem) |> ignore
            
    let insertCartItem connString (cartInfo:CartInfo):Result<unit, string> =        
        try
            ( use connection = new SqlConnection(connString)
              connection.EnsureOpen() |> ignore
              ( use trans = connection.BeginTransaction()
                connection.Update<CartInfo>(cartInfo, cartInfo.Id) |> ignore
                cartInfo.LineItems |> List.map (insertLineItem connection)
                trans.Commit() )
            )
            Ok ()
        with
            | :? Exception as ex -> Error ex.Message
            
