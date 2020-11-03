namespace DuxCommerce.Catalogue

open System
open System.Data.SqlClient
open DuxCommerce.Catalogue.Dto
open DuxCommerce.Common
open RepoDb
open System.Linq
open DuxCommerce.Catalogue.SimpleTypes
open DuxCommerce.Catalogue.PublicTypes

module ProductRepo =
    
    let createProduct connString product :Result<ProductInfo, CustomError> =
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
            | :? Exception as ex -> Error ex |> CustomError.mapInternalServer
            
    let getProduct connString productId :Result<ProductInfo, CustomError>=
        try
            let id = ProductId.value productId
            ( use connection = new SqlConnection(connString)
              let info = connection.Query<ProductInfo>(fun p -> p.Id = id).FirstOrDefault()
              Ok info
            )
        with
            | :? Exception as ex -> Error ex |> CustomError.mapInternalServer
            
    let updateProduct connString productId product :Result<unit, CustomError>=
        try
            let info = ProductInfo.fromDomain product
            let id = ProductId.value productId
            ( use connection = new SqlConnection(connString)
              connection.Update<ProductInfo>(info, id) |> ignore
              Ok ()
            )
        with
            | :? Exception as ex -> Error ex |> CustomError.mapInternalServer
