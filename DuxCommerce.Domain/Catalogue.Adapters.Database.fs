namespace DuxCommerce.Catalogue

open System
open System.Data.SqlClient
open RepoDb
module DataAccess =
    
    let createProduct connString (product:CreateProductDto) =
        try
            ( use connection = new SqlConnection(connString)
              connection.EnsureOpen() |> ignore
              ( use trans = connection.BeginTransaction()
                connection.Insert<CreateProductDto, int> (product, transaction = trans) |> ignore
                trans.Commit() )
            )
            Ok ()
        with
            | :? Exception as ex -> Error ex.Message