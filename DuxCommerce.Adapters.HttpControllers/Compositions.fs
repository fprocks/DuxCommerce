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
    let getProduct : GetProduct  = getProduct Constants.connString    
    let insertCartItem : InsertCartItem= insertCartItem Constants.connString
    let getShoppingCart: GetShopperCart = getShoppingCart Constants.connString