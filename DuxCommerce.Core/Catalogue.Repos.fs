namespace DuxCommerce.Catalogue

open System.Data.SqlClient
open DuxCommerce.Catalogue.Dto
open DuxCommerce.Common
open RepoDb
open System.Linq
open DuxCommerce.Catalogue.SimpleTypes
open DuxCommerce.Catalogue.PublicTypes
open DuxCommerce.Catalogue.Ports

module ProductRepo =
    
    let createProduct connString :CreateProduct =
        fun product ->
            let create product = 
                let info = ProductInfo.fromDomain product
                ( use connection = new SqlConnection(connString)
                  connection.EnsureOpen() |> ignore
                  connection.Insert<ProductInfo, int64>(info) |> ignore
                )
                info.Id
                
            RepoAdapter.repoAdapter1 create product
            
    let getProduct connString :GetProduct =
        fun productId ->
            let get productId =
                let id = ProductId.value productId
                ( use connection = new SqlConnection(connString)
                  connection.EnsureOpen() |> ignore
                  connection.Query<ProductInfo>(fun p -> p.Id = id).FirstOrDefault()
                )
                
            RepoAdapter.repoAdapter1 get productId            
            
    let updateProduct connString :UpdateProduct =
        fun productId product ->
            let update productId product =
                let info = ProductInfo.fromDomain product
                let id = ProductId.value productId
                ( use connection = new SqlConnection(connString)
                  connection.EnsureOpen() |> ignore
                  connection.Update<ProductInfo>(info, id) |> ignore
                )
                
            RepoAdapter.repoAdapter2 update productId product