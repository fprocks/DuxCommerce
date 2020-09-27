namespace DuxCommerce.Catalogue

open System
open System.Data.SqlClient
open RepoDb
module DataAccess =
    
    let createProduct connString (product:CreateProductRequest) =
        try
            ( use connection = new SqlConnection(connString)
              connection.EnsureOpen() |> ignore
              ( use trans = connection.BeginTransaction()
                connection.Insert<CreateProductRequest, int64> (product, transaction = trans) |> ignore
                trans.Commit() )
            )
            Ok ()
        with
            | :? Exception as ex -> Error ex.Message
            
            
    let getProducts connString id =
        try
            ( use connection = new SqlConnection(connString)
              let product = connection.Query<CreateProductRequest>(fun p -> p.Id = id)
              Ok product
            )
        with
            | :? Exception as ex -> Error ex.Message
