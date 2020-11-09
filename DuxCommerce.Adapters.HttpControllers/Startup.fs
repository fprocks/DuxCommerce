namespace WebApi

open System.Reflection
open DuxCommerce.Adapters.HttpControllers
open DuxCommerce.DatabaseMigrations
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open FluentMigrator.Runner
open RepoDb
open DuxCommerce.Common.ConfigReader
open DuxCommerce.Common

module DbSetup = 
    let InitDb =   
        ModelMapper.Map()
        SqlServerBootstrap.Initialize()
        
type Startup private () =
    new (configuration: IConfiguration) as this =
        Startup() then
        this.Configuration <- configuration

    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services: IServiceCollection) =
        // Add framework services.
        services.AddAuthorization() |> ignore
        services.AddControllers().AddNewtonsoftJson() |> ignore
        
        DbSetup.InitDb

        let config:AppConfig = {
            ConnectionString = "Server=(local);Database=DuxCommerce;User Id=DuxAdmin;Password=Password1;"
        }
        
        let configClient = new ConfigClient()
        configClient.Set(config)
        
        services.AddFluentMigratorCore().ConfigureRunner(fun config ->
            config.AddSqlServer()
                .WithGlobalConnectionString(Constants.connString)
                .ScanIn(Assembly.Load("DuxCommerce.DatabaseMigrations")).For.All() |> ignore
            ).AddLogging(fun config -> config.AddFluentMigratorConsole() |> ignore) |> ignore

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore

        app.UseHttpsRedirection() |> ignore
        app.UseRouting() |> ignore

        app.UseAuthorization() |> ignore

        app.UseEndpoints(fun endpoints ->
            endpoints.MapControllers() |> ignore
            ) |> ignore
        
        app.ApplicationServices
           .CreateScope()
           .ServiceProvider.GetService<IMigrationRunner>()
           .MigrateUp() |> ignore


    member val Configuration : IConfiguration = null with get, set
