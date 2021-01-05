namespace DuxCommerce.Catalogue

open System.Data.SqlClient
open DuxCommerce.Core.Common
open RepoDb
open System.Linq
open DuxCommerce.Catalogue.PublicTypes
open DuxCommerce.Catalogue.Ports

module ProductRepo =
    
    let createProduct :CreateProduct =
        fun productDto ->
            let create (connection:SqlConnection) = 
                connection.Insert<ProductDto, int64>(productDto) |> ignore
                productDto.Id
                
            SqlRepoAdapter.repoAdapter create
            
    let getProduct :GetProduct =
        fun id ->
            let get (connection:SqlConnection) =
                connection.Query<ProductDto>(fun p -> p.Id = id).FirstOrDefault()
                
            SqlRepoAdapter.repoAdapter get          
            
    let updateProduct :UpdateProduct =
        fun id productDto ->
            let update (connection:SqlConnection) =
                connection.Update<ProductDto>(productDto, id) |> ignore
                
            SqlRepoAdapter.repoAdapter update