namespace DuxCommerce.ShoppingCarts

open System
open System.Data.SqlClient
open RepoDb

module ShoppingCartDb =
    
    let saveCartItem (connection:SqlConnection) (cartItem:CartItemInfo) =
        if cartItem.Id > 0L then
            connection.Update<CartItemInfo>(cartItem, cartItem.Id) |> ignore
        else
            connection.Insert<CartItemInfo, int64>(cartItem) |> ignore
            
    let saveCart connString (cartInfo:CartInfo):Result<CartInfo, string> =        
        try
            ( use connection = new SqlConnection(connString)
              connection.EnsureOpen() |> ignore
              ( use trans = connection.BeginTransaction()
                connection.Update<CartInfo>(cartInfo, cartInfo.Id) |> ignore
                cartInfo.LineItems |> List.map (saveCartItem connection)
                trans.Commit() )
            )
            Ok cartInfo
        with
            | :? Exception as ex -> Error ex.Message
            
