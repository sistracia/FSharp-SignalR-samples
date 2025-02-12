open StockTickR
open StockTickR.Hubs
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)

    builder.Services.AddSignalR().AddMessagePackProtocol() |> ignore
    builder.Services.AddSingleton<StockTicker>() |> ignore

    builder.Services.AddCors(fun options ->
        options.AddPolicy(
            "CorsPolicy",
            fun builder ->
                builder
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .WithMethods("GET", "POST")
                    .AllowCredentials()
                |> ignore
        ))
    |> ignore

    let app = builder.Build()

    app.UseCors("CorsPolicy") |> ignore
    app.UseRouting() |> ignore

    app.UseEndpoints(fun endpoints -> endpoints.MapHub<StockTickerHub>("/stocks") |> ignore)
    |> ignore

    app.Run()

    0 // Exit code
