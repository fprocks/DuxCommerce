namespace WebApi

open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open RepoDb
open DuxCommerce.DatabaseMigrations

module Program =
       
    let CreateHostBuilder args =
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(fun webBuilder ->
                webBuilder.UseStartup<Startup>() |> ignore
            )

    [<EntryPoint>]
    let main args =
                
        CreateHostBuilder(args).Build().Run()

        0
