module PlaceOrder

open System.Data.SqlClient
open System.Threading.Tasks
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
      connection.EnsureOpen() |> ignore
      ( use trans = connection.BeginTransaction()
        connection.Insert<Product, int> (product, transaction = trans) |> ignore
        connection.Insert<Customer, int> (customer, transaction = trans) |> ignore
        trans.Commit() )
    )
    
let insert2 connString (product:Product) (customer:Customer) =
    using(new SqlConnection(connString)) (fun connection ->
        connection.EnsureOpen() |> ignore
        using(connection.BeginTransaction()) (fun trans ->
            connection.Insert<Product, int> (product, transaction = trans) |> ignore
            connection.Insert<Customer, int> (customer, transaction = trans) |> ignore
            trans.Commit()
        )
    )
        
type IStorage =
    abstract member LoadProduct : int -> Task<Product>
    abstract member CreateProduct : Product -> Task<unit>
    abstract member LoadCustomer : int -> Task<Customer>
    abstract member CreateCustomer : Customer -> Task<unit>
    