namespace DuxCommerce.Adapters.HttpControllers

open DuxCommerce.Catalogue
open DuxCommerce.Catalogue.InternalTypes
open DuxCommerce.ShoppingCarts

module Constants =
    let connString = "Server=(local);Database=DuxCommerce;User Id=DuxAdmin;Password=Password1;"

module CatalogueComposition =
    let createProduct = ProductRepo.createProduct Constants.connString
    let getProduct = ProductRepo.getProduct Constants.connString
    let updateProduct = ProductRepo.updateProduct Constants.connString
    
    let createProduct' = UseCases.createProduct createProduct getProduct    
    let getProduct' = UseCases.getProduct getProduct
    let updateProduct' = UseCases.updateProduct updateProduct getProduct
    
module ShoppingCartComposition =
    // Todo: pass in ShopperId from shopper context
    let getShoppingCart  = CartRepo.getShoppingCart Constants.connString
    let getProduct   = ProductRepo.getProduct Constants.connString    
    let saveCart  = CartRepo.saveCart Constants.connString    
    let deleteItem   = CartRepo.deleteItem Constants.connString
    
    let addCartItem = UseCases.addCartItem getShoppingCart getProduct saveCart   
    let updateCart = UseCases.updateCart getShoppingCart saveCart
    let deleteCartItem = UseCases.deleteCartItem getShoppingCart deleteItem
    