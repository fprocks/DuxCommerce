namespace DuxCommerce.Catalogue

open System
open System.Data.SqlClient
open RepoDb
module DataAccess =
    
    let createProduct connString product =
        try
            ( use connection = new SqlConnection(connString)
              connection.EnsureOpen() |> ignore
              ( use trans = connection.BeginTransaction()
                connection.Insert<ProductInfo, int64> (product, transaction = trans) |> ignore
                trans.Commit() )
            )
            Ok product
        with
            | :? Exception as ex -> Error ex.Message
            
            
    let getProduct connString id =
        try
            ( use connection = new SqlConnection(connString)
              let product = connection.Query<ProductInfo>(fun p -> p.Id = id)
              Ok product
            )
        with
            | :? Exception as ex -> Error ex.Message                       
            
    let updateProduct connString product =
        try
            ( use connection = new SqlConnection(connString)
              connection.Update<ProductInfo>(product) |> ignore
              Ok product
            )
        with
            | :? Exception as ex -> Error ex.Message            
