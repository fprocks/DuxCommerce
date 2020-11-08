namespace DuxCommerce.Catalogue

open System.Data.SqlClient
open DuxCommerce.Core.Common
open RepoDb
open System.Linq
open DuxCommerce.Catalogue.PublicTypes
open DuxCommerce.Catalogue.Ports

module ProductRepo =
    
    let createProduct connString :CreateProduct =
        fun productDto ->
            let create (connection:SqlConnection) productDto = 
                connection.Insert<ProductDto, int64>(productDto) |> ignore
                productDto.Id
                
            RepoAdapter.repoAdapter1 connString create productDto
            
    let getProduct connString :GetProduct =
        fun id ->
            let get (connection:SqlConnection) id =
                connection.Query<ProductDto>(fun p -> p.Id = id).FirstOrDefault()
                
            RepoAdapter.repoAdapter1 connString get id            
            
    let updateProduct connString :UpdateProduct =
        fun id productDto ->
            let update (connection:SqlConnection) (id:int64) productDto =
                connection.Update<ProductDto>(productDto, id) |> ignore
                
            RepoAdapter.repoAdapter2 connString update id productDto