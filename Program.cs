using System.Net;
using WasiOrleans;

await Host.CreateDefaultBuilder(args)
    .UseOrleans((ctx, silo) =>
    {
        var instanceId = ctx.Configuration.GetValue<int>("InstanceId");
        var port = 11_111;

        silo.UseLocalhostClustering(
            siloPort: port + instanceId,
            gatewayPort: 30_000 + instanceId,
            primarySiloEndpoint: new IPEndPoint(IPAddress.Loopback, port)
        );

        silo.UseDashboard();

        // Enable distributed logging;
        silo.AddActivityPropagation();
    })
    // .ConfigureWebHostDefaults(webBuilder => {
    //     webBuilder.UseStartup<Startup>();

    // })
    .RunConsoleAsync();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
