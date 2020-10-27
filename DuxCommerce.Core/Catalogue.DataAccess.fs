namespace DuxCommerce.Catalogue

open System
open System.Data.SqlClient
open DuxCommerce.Catalogue.Dto
open RepoDb
open System.Linq

module CatalogueDb =
    
    let createProduct connString product =
        try
            let info = ProductInfo.fromDomain product
            ( use connection = new SqlConnection(connString)
              connection.EnsureOpen() |> ignore
              ( use trans = connection.BeginTransaction()
                connection.Insert<ProductInfo, int64> (info, transaction = trans) |> ignore
                trans.Commit() )
            )
            Ok info
        with
            | :? Exception as ex -> Error ex.Message
            
    let getProduct connString productId =
        try
            let id = ProductId.value productId
            ( use connection = new SqlConnection(connString)
              let product = connection.Query<ProductInfo>(fun p -> p.Id = id).FirstOrDefault()
              Ok product
            )
        with
            | :? Exception as ex -> Error ex.Message                       
            
    let updateProduct connString productId product =
        try
            let info = ProductInfo.fromDomain product
            let id = ProductId.value productId
            ( use connection = new SqlConnection(connString)
              connection.Update<ProductInfo>(info, id) |> ignore
              Ok ()
            )
        with
            | :? Exception as ex -> Error ex.Message            
