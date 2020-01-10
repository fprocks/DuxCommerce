module PlaceOrder

open FSharp.Data.Sql

let [<Literal>] connStr = "Server=(local);Database=DuxCommerce;User Id=DuxAdmin;Password=Password1;"
type DuxCommerce = SqlDataProvider<ConnectionString = connStr, UseOptionTypes = true>

let context = DuxCommerce.GetDataContext()

let insertCustomer = 
    let product = context.Dbo.Products.Create()
    product.Name <- "DDD"
    product.Price <- 45m
    context.SubmitUpdates

let getProduct (name:string) = 
    query {
        for product in context.Dbo.Products do
        where (product.Name.Equals(name))
        take 1
        select product
    }