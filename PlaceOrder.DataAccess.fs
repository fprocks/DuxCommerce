module PlaceOrder

open System.Data.SqlClient
open RepoDb

type Product = {
    Id : int
    Name : string
    Price: decimal
}

type Customer = {
    Id : int
    Name : string
}

let insert1 connString (product:Product) (customer:Customer) =
    ( use connection = new SqlConnection(connString)
      connection.EnsureOpen()
      ( use trans = connection.BeginTransaction()
        connection.Insert<Product, int> (product, transaction = trans)
        connection.Insert<Customer, int> (customer, transaction = trans)
        trans.Commit() )
    )
    
let insert2 connString (product:Product) (customer:Customer) =
    using(new SqlConnection(connString)) (fun connection ->
        connection.EnsureOpen()
        using(connection.BeginTransaction()) (fun trans ->
            connection.Insert<Product, int> (product, transaction = trans)
            connection.Insert<Customer, int> (customer, transaction = trans)
            trans.Commit()
        )
    )