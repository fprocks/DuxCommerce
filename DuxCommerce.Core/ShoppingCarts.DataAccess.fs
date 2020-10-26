namespace DuxCommerce.ShoppingCarts

open System
open System.Data.SqlClient
open DuxCommerce.ShoppingCarts
open DuxCommerce.ShoppingCarts.Dto
open RepoDb
open System.Linq

module ShoppingCartDb =
    
    let internal insertLineItem (connection:SqlConnection) (trans:SqlTransaction) (cartItem:CartItemInfo) =
        if cartItem.Id = 0L then
            connection.Insert<CartItemInfo, int64>(cartItem, transaction = trans) |> ignore
            
    let saveCartItem connString (cart:DomainTypes.Cart) :Result<unit, string> =        
        try
            let cartInfo = ShoppingCart.fromDomain cart
            ( use connection = new SqlConnection(connString)
              connection.EnsureOpen() |> ignore
              ( use trans = connection.BeginTransaction()
                connection.Update<CartInfo>(cartInfo, cartInfo.Id, transaction = trans) |> ignore
                cartInfo.LineItems |> List.map (insertLineItem connection trans) |> ignore
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
              | null -> 
                        let newCart: CartInfo = {Id = 0L; ShopperId = id; LineItems = []; CartTotal = 0.0M}
                        connection.Insert<CartInfo, int64>(newCart) |> ignore
                        Ok newCart
              | _ -> 
                        let items = connection.Query<CartItemInfo>(fun c -> c.CartId = cartInfo.Id)
                        let newCart = { cartInfo with LineItems = (List.ofSeq items)}
                        Ok newCart
                    
            )
        with
            | :? Exception as ex -> Error ex.Message
            
