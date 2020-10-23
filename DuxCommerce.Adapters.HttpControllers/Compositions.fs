namespace DuxCommerce.Adapters.HttpControllers

open DuxCommerce.Catalogue
open DuxCommerce.ShoppingCarts
open UseCases

module Constants =
    let connString = "Server=(local);Database=DuxCommerce;User Id=DuxAdmin;Password=Password1;"

module CatalogueComposition =
    let createProduct = createProduct Constants.connString    
    let getProduct = getProduct Constants.connString
    let updateProduct = updateProduct Constants.connString
    
module ShoppingCartComposition =
    let getProduct  = getProduct Constants.connString
    
    let addCartItem = addCartItem 