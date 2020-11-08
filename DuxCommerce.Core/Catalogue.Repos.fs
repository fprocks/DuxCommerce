namespace DuxCommerce.Catalogue

open System.Data.SqlClient
open DuxCommerce.Common
open RepoDb
open System.Linq
open DuxCommerce.Catalogue.PublicTypes
open DuxCommerce.Catalogue.Ports

module ProductRepo =
    
    let createProduct connString :CreateProduct =
        fun productInfo ->
            let create productInfo = 
                ( use connection = new SqlConnection(connString)
                  connection.EnsureOpen() |> ignore
                  connection.Insert<ProductInfo, int64>(productInfo) |> ignore
                )
                productInfo.Id
                
            RepoAdapter.repoAdapter1 create productInfo
            
    let getProduct connString :GetProduct =
        fun id ->
            let get id =
                ( use connection = new SqlConnection(connString)
                  connection.EnsureOpen() |> ignore
                  connection.Query<ProductInfo>(fun p -> p.Id = id).FirstOrDefault()
                )
                
            RepoAdapter.repoAdapter1 get id            
            
    let updateProduct connString :UpdateProduct =
        fun id productInfo ->
            let update (id:int64) productInfo =
                ( use connection = new SqlConnection(connString)
                  connection.EnsureOpen() |> ignore
                  connection.Update<ProductInfo>(productInfo, id) |> ignore
                )
                
            RepoAdapter.repoAdapter2 update id productInfo