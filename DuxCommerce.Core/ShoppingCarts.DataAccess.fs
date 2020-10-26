namespace DuxCommerce.ShoppingCarts

open System
open System.Data.SqlClient
open DuxCommerce.ShoppingCarts
open DuxCommerce.ShoppingCarts.Dto
open RepoDb
open System.Linq

module ShoppingCartDb =
    
    let internal insertLineItem (connection:SqlConnection) (cartItem:CartItemInfo) =
        if cartItem.Id = 0L then
            connection.Insert<CartItemInfo, int64>(cartItem) |> ignore
            
    let saveCartItem connString (cart:DomainTypes.Cart) :Result<unit, string> =        
        try
            let cartInfo = ShoppingCart.fromDomain cart
            ( use connection = new SqlConnection(connString)
              connection.EnsureOpen() |> ignore
              ( use trans = connection.BeginTransaction()
                connection.Update<CartInfo>(cartInfo, cartInfo.Id) |> ignore
                cartInfo.LineItems |> List.map (insertLineItem connection) |> ignore
                trans.Commit() )
            )
            Ok ()
        with
            | :? Exception as ex -> Error ex.Message
            
    let getShoppingCart connString (shopperId:ShopperId) : Result<CartInfo, string> =
        try
            let id = ShopperId.value shopperId
            ( use connection = new SqlConnection(connString)
              connection.EnsureOpen() |> ignore
              let cartInfo = connection.Query<CartInfo>(fun c -> c.ShopperId = id).FirstOrDefault()
              match box cartInfo with
              | null -> Ok cartInfo
              | _ ->
                    let newCart: CartInfo = {Id = 0L; ShopperId = id; LineItems = []; CartTotal = 0.0M}
                    connection.Insert<CartInfo, int64>(newCart) |> ignore
                    Ok newCart
            )
        with
            | :? Exception as ex -> Error ex.Message
            
