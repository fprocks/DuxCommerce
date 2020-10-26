namespace DuxCommerce.Catalogue

open System
open System.Data.SqlClient
open RepoDb
open System.Linq

module CatalogueDb =
    
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
              let product = connection.Query<ProductInfo>(fun p -> p.Id = id).FirstOrDefault()
              // Todo: improve to handle null value
              Ok product
            )
        with
            | :? Exception as ex -> Error ex.Message                       
            
    let updateProduct connString (id:int64) product =
        try
            ( use connection = new SqlConnection(connString)
              connection.Update<ProductInfo>(product, id) |> ignore
              // Todo: how to return updated product
              Ok ()
            )
        with
            | :? Exception as ex -> Error ex.Message            
