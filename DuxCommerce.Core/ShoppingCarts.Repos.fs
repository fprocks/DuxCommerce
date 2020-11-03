namespace DuxCommerce.ShoppingCarts

open System
open System.Data.SqlClient
open DuxCommerce.Common
open DuxCommerce.ShoppingCarts
open DuxCommerce.ShoppingCarts.Dto
open RepoDb
open System.Linq
open DuxCommerce.ShoppingCarts.SimpleTypes
open DuxCommerce.ShoppingCarts.PublicTypes

module CartRepo =
    
    let internal insertOrUpdate (connection:SqlConnection) (trans:SqlTransaction) (itemInfo:CartItemInfo) =
        if itemInfo.Id = 0L 
        then connection.Insert<CartItemInfo, int64>(itemInfo, transaction = trans) |> ignore
        else connection.Update<CartItemInfo>(itemInfo, itemInfo.Id, transaction = trans) |>ignore
            
    let saveCart connString (cart:InternalTypes.Cart) :Result<unit, CustomError> =        
        try
            let cartInfo = ShoppingCart.fromDomain cart
            ( use connection = new SqlConnection(connString)
              connection.EnsureOpen() |> ignore
              ( use trans = connection.BeginTransaction()
                connection.Update<CartInfo>(cartInfo, cartInfo.Id, transaction = trans) |> ignore
                
                // Todo: why will LineItems not be saved when changing Seq.iter to Seq.map
                cartInfo.LineItems |> Seq.iter (insertOrUpdate connection trans) |> ignore
                trans.Commit() )
            )
            Ok ()
        with
            | :? Exception as ex -> Error ex |> CustomError.mapInternalServer
            
    let getShoppingCart connString (shopperId:ShopperId) :Result<CartInfo, CustomError> =
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
                        let newCart = { cartInfo with LineItems = items}
                        Ok newCart                    
            )
        with
            | :? Exception as ex -> Error ex |> CustomError.mapInternalServer
    
    let deleteItem connString (updatedCart, deletedItems: InternalTypes.CartItem seq) :Result<unit, CustomError> =        
        try
            let cartInfo = ShoppingCart.fromDomain updatedCart
            ( use connection = new SqlConnection(connString)
              connection.EnsureOpen() |> ignore
              ( use trans = connection.BeginTransaction()
                connection.Update<CartInfo>(cartInfo, cartInfo.Id, transaction = trans) |> ignore
                                
                deletedItems
                |> Seq.map CartItem.fromDomain
                |> Seq.iter (fun item -> connection.Delete<CartItemInfo>(item.Id, transaction = trans) |> ignore)
                |> ignore
                
                trans.Commit() )
            )
            Ok ()
        with
            | :? Exception as ex -> Error ex |> CustomError.mapInternalServer