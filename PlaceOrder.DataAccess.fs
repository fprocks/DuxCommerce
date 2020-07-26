module PlaceOrder

open FSharp.Data.Sql

let [<Literal>] connStr = "Server=(local);Database=DuxCommerce;User Id=DuxAdmin;Password=Password1;"
type DuxCommerce = SqlDataProvider<Common.DatabaseProviderTypes.MSSQLSERVER, 
                                   ConnectionString = connStr, 
                                   UseOptionTypes = true>

let context = DuxCommerce.GetDataContext()

let insertProduct id name price = 
    let product = context.Dbo.Products.Create()
    product.Id <- id
    product.Name <- name
    product.Price <- price
    context.SubmitUpdates

let getProduct (name:string) = 
    query {
        for product in context.Dbo.Products do
        where (product.Name = name)
        select product
    } |> Seq.toList