namespace DuxCommerce.Adapters.HttpControllers

open DuxCommerce.Catalogue
open DuxCommerce.Catalogue.Ports
open DuxCommerce.Common
open DuxCommerce.ShoppingCarts
open DuxCommerce.ShoppingCarts.InternalTypes
open DuxCommerce.ShoppingCarts.Ports

module Constants =
    let connString = "Server=(local);Database=DuxCommerce;User Id=DuxAdmin;Password=Password1;"

module Compositions =
    // Todo: pass in adminId from store admin context
    let private createProduct :CreateProduct = ProductRepo.createProduct Constants.connString
    let private getProduct :GetProduct = ProductRepo.getProduct Constants.connString
    let private updateProduct :UpdateProduct = ProductRepo.updateProduct Constants.connString

    // Todo: pass in ShopperId from shopper context
    let private getShoppingCart :GetShoppingCart = CartRepo.getShoppingCart Constants.connString
    let private saveCart :SaveCart = CartRepo.saveCart Constants.connString
    
    type DeleteCartItem = Cart * (CartItem seq) -> Result<uint, CustomError>
    let private deleteItem  = CartRepo.deleteItem Constants.connString
    
    let createProduct' = UseCases.createProduct' createProduct getProduct    
    let getProduct' = UseCases.getProduct' getProduct
    let updateProduct' = UseCases.updateProduct' updateProduct getProduct
    
    let addCartItem' = UseCases.addCartItem getShoppingCart getProduct saveCart   
    let updateCart' = UseCases.updateCart getShoppingCart saveCart
    let deleteCartItem' = UseCases.deleteCartItem getShoppingCart deleteItem
    