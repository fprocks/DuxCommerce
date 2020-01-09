module PlaceOrder

open FSharp.Data.Sql

let [<Literal>] connStr = "Server=(local);Database=DuxCommerce;User Id=DuxAdmin;Password=Password1;"
type DuxCommerce = SqlDataProvider<ConnectionString = connStr, UseOptionTypes = true>
//type DuxCommerce = SqlDataProvider<ConnectionString = connStr, UseOptionTypes = true>

let context = DuxCommerce.GetDataContext()