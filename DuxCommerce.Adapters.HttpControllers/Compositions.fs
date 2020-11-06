namespace DuxCommerce.Adapters.HttpControllers

open DuxCommerce.Catalogue
open DuxCommerce.ShoppingCarts

module Constants =
    let connString = "Server=(local);Database=DuxCommerce;User Id=DuxAdmin;Password=Password1;"

module Compositions =
    // Todo: pass in adminId from store admin context
    let private createProduct = ProductRepo.createProduct Constants.connString
    let private getProduct = ProductRepo.getProduct Constants.connString
    let private updateProduct = ProductRepo.updateProduct Constants.connString

    // Todo: pass in ShopperId from shopper context
    let private getShoppingCart = CartRepo.getShoppingCart Constants.connString
    let private saveCart = CartRepo.saveShoppingCart Constants.connString    
    let private deleteItem  = CartRepo.deleteCartItem Constants.connString
    
    let createProductUseCase = UseCases.createProduct createProduct getProduct  
    let getProductUseCase = UseCases.getProduct getProduct
    let updateProductUseCase = UseCases.updateProduct updateProduct getProduct
    
    let addCartItemUseCase = UseCases.addCartItem getShoppingCart getProduct saveCart   
    let updateCartUseCase = UseCases.updateCart getShoppingCart saveCart
    let deleteCartItemUseCase = UseCases.deleteCartItem getShoppingCart deleteItem
    