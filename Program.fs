open PlaceOrder
let connString = "Server=(local);Database=DuxCommerce;User Id=DuxAdmin;Password=Password1;"

[<EntryPoint>]
let main argv =
    RepoDb.SqlServerBootstrap.Initialize()
    
    let product = {Id=12; Name="BDD"; Price=100m}
    let customer = {Id = 10; Name="Bill"}
    insert connString product customer

    0
