namespace DuxCommerce.Adapters.HttpControllers

open DuxCommerce.Catalogue
open DuxCommerce.ShoppingCarts
open DuxCommerce.ShoppingCarts.DomainTypes
open UseCases
open ShoppingCartDb
open ShoppingCart

module Constants =
    let connString = "Server=(local);Database=DuxCommerce;User Id=DuxAdmin;Password=Password1;"

module CatalogueComposition =
    let createProduct = createProduct Constants.connString    
    let getProduct = getProduct Constants.connString
    let updateProduct = updateProduct Constants.connString
    
module ShoppingCartComposition =
    // Todo: pass in ShopperId from shopper context
    let getShoppingCart : GetShopperCart = getShoppingCart Constants.connString (ShopperId.create 100L)
    let getProduct : GetProduct  = getProduct Constants.connString    
    let saveCartItem : SaveCartItem = saveCartItem Constants.connString    
    let addCartItem = UseCases.addCartItem getShoppingCart getProduct addCartItem saveCartItem    