namespace DuxCommerce.Adapters.HttpControllers

open System
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open WebApi

[<ApiController>]
[<Route("api/v1/[controller]")>]
type WeatherForecastController (logger : ILogger<WeatherForecastController>) =
    inherit ControllerBase()
    let summaries = [| "Freezing"; "Bracing"; "Chilly"; "Cool"; "Mild"; "Warm"; "Balmy"; "Hot"; "Sweltering"; "Scorching" |]
    
    [<HttpGet>]
    member this.Get() : WeatherForecast[] =
        let rng = System.Random()
        [|
            for index in 0..4 ->
                { Date = DateTime.Now.AddDays(float index)
                  TemperatureC = rng.Next(-20,55)
                  Summary = summaries.[rng.Next(summaries.Length)] }
        |]
