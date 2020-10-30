namespace DuxCommerce.Adapters.HttpControllers

open DuxCommerce.Catalogue
open DuxCommerce.ShoppingCarts
open UseCases
open ShoppingCartDb

module Constants =
    let connString = "Server=(local);Database=DuxCommerce;User Id=DuxAdmin;Password=Password1;"

module CatalogueComposition =
    let createProduct = createProduct Constants.connString    
    let getProduct = getProduct Constants.connString
    let updateProduct = updateProduct Constants.connString
    
module ShoppingCartComposition =
    // Todo: pass in ShopperId from shopper context
    let getShoppingCart  = getShoppingCart Constants.connString
    let getProduct   = getProduct Constants.connString    
    let saveCart  = saveCart Constants.connString    
    let deleteItem   = deleteItem Constants.connString
    
    let addCartItem = UseCases.addCartItem getShoppingCart getProduct saveCart   
    let updateCart = UseCases.updateCart getShoppingCart saveCart
    let deleteCartItem = UseCases.deleteCartItem getShoppingCart deleteItem
    