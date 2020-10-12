namespace DuxCommerce.Adapters.HttpControllers

open DuxCommerce.Catalogue
open UseCases

module CompositionRoot =
    let connString = "Server=(local);Database=DuxCommerce;User Id=DuxAdmin;Password=Password1;"

    let createProduct = createProduct connString    
    let getProduct = getProduct connString
    let updateProduct = updateProduct connString