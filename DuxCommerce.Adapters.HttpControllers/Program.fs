namespace WebApi

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Threading.Tasks
open DuxCommerce.Catalogue
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open RepoDb

module Program =
    
    let InitDb =
        SqlServerBootstrap.Initialize()
        FluentMapper.Entity<CreateProductDto>().Table("Product") |> ignore
    
    let CreateHostBuilder args =
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(fun webBuilder ->
                webBuilder.UseStartup<Startup>() |> ignore
            )

    [<EntryPoint>]
    let main args =
        
        InitDb
        
        CreateHostBuilder(args).Build().Run()

        0
