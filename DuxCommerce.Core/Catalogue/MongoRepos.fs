namespace DuxCommerce.Core.Catalogue.MongoRepos

open DuxCommerce.Common
open DuxCommerce.Core.Catalogue.PublicTypes
open MongoDB.Driver
open DuxCommerce.Core.Common

type CreateProduct = ProductDto -> ConfigReader<Result<string, CustomError>>
type GetProduct = string -> ConfigReader<Result<ProductDto, CustomError>>
type UpdateProduct = string -> ProductDto -> ConfigReader<Result<unit, CustomError>>

module ProductRepo =
    
    let createProduct :CreateProduct =
        fun productDto ->
            let create (db:IMongoDatabase) = 
                let products = db.GetCollection<ProductDto>(CollectionName.Product)
                products.InsertOne(productDto)
                productDto.Id
                
            MongoRepoAdapter.repoAdapter create
            
    let getProduct :GetProduct =
        fun id ->
            let get (db:IMongoDatabase) =
                let products = db.GetCollection<ProductDto>(CollectionName.Product)
                products.Find(fun x -> x.Id = id).FirstOrDefault()
                
            MongoRepoAdapter.repoAdapter get          
            
    let updateProduct :UpdateProduct =
        fun id productDto ->
            let update (db:IMongoDatabase) =
                let products = db.GetCollection<ProductDto>(CollectionName.Product)
                let productDto = {productDto with Id = id}
                products.ReplaceOne((fun x -> x.Id = id), productDto) |> ignore

            MongoRepoAdapter.repoAdapter update