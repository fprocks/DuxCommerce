namespace DuxCommerce.ShoppingCarts

open System.Data.SqlClient
open DuxCommerce.Core.Common
open DuxCommerce.ShoppingCarts.Ports
open RepoDb
open System.Linq
open DuxCommerce.ShoppingCarts.PublicTypes

module CartRepo =
                
    let saveShoppingCart :SaveShoppingCart =
        fun cartDto ->      

            let insertOrUpdate (connection:SqlConnection) (cartItemDto:CartItemDto) =
                if cartItemDto.Id = 0L 
                then connection.Insert<CartItemDto, int64>(cartItemDto) |> ignore
                else connection.Update<CartItemDto>(cartItemDto, cartItemDto.Id) |>ignore
                
            let save (connection:SqlConnection) cartDto =
                connection.Update<ShoppingCartDto>(cartDto, cartDto.Id) |> ignore

                cartDto.LineItems 
                |> Seq.iter (insertOrUpdate connection) 
                |> ignore

            RepoAdapter.repoAdapter1 save cartDto
            
    let getShoppingCart :GetShoppingCart = 
        fun shopperId ->
            let get (connection:SqlConnection) shopperId =
                let cartDto = connection.Query<ShoppingCartDto>(fun c -> c.ShopperId = shopperId).FirstOrDefault()
                match box cartDto with
                | null -> 
                    let newCart: ShoppingCartDto = {Id = 0L; ShopperId = shopperId; LineItems = []; CartTotal = 0.0M}
                    connection.Insert<ShoppingCartDto, int64>(newCart) |> ignore
                    newCart
                | _ -> 
                    let items = connection.Query<CartItemDto>(fun c -> c.CartId = cartDto.Id)
                    { cartDto with LineItems = items}
                
            RepoAdapter.repoAdapter1 get shopperId
    
    let deleteCartItem =
        fun (cartToUpdate, itemsToDelete: CartItemDto seq) ->        
            let delete (connection:SqlConnection) (cartToUpdate, itemsToDelete : CartItemDto seq) =
                ( use trans = connection.BeginTransaction()
                  connection.Update<ShoppingCartDto>(cartToUpdate, cartToUpdate.Id, transaction = trans) |> ignore
                                  
                  itemsToDelete
                  |> Seq.iter (fun item -> connection.Delete<CartItemDto>(item.Id, transaction = trans) |> ignore)
                  |> ignore
                  
                  trans.Commit() )
                
            RepoAdapter.repoAdapter1 delete (cartToUpdate, itemsToDelete)