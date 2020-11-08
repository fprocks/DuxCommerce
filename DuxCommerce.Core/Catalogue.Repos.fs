namespace DuxCommerce.Catalogue

open System.Data.SqlClient
open DuxCommerce.Core.Common
open RepoDb
open System.Linq
open DuxCommerce.Catalogue.PublicTypes
open DuxCommerce.Catalogue.Ports

module ProductRepo =
    
    let createProduct connString :CreateProduct =
        fun productInfo ->
            let create (connection:SqlConnection) productInfo = 
                connection.Insert<ProductInfo, int64>(productInfo) |> ignore
                productInfo.Id
                
            RepoAdapter.repoAdapter1 connString create productInfo
            
    let getProduct connString :GetProduct =
        fun id ->
            let get (connection:SqlConnection) id =
                connection.Query<ProductInfo>(fun p -> p.Id = id).FirstOrDefault()
                
            RepoAdapter.repoAdapter1 connString get id            
            
    let updateProduct connString :UpdateProduct =
        fun id productInfo ->
            let update (connection:SqlConnection) (id:int64) productInfo =
                connection.Update<ProductInfo>(productInfo, id) |> ignore
                
            RepoAdapter.repoAdapter2 connString update id productInfo