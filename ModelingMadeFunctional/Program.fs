open PlaceOrder
let connString = "Server=(local);Database=DuxCommerce;User Id=DuxAdmin;Password=Password1;"

[<EntryPoint>]
let main argv =
    RepoDb.SqlServerBootstrap.Initialize()
    
    let product = {Id=14; Name="BDD"; Price=100m}
    let customer = {Id = 14; Name="Bill"}
    insert1 connString product customer

    0
