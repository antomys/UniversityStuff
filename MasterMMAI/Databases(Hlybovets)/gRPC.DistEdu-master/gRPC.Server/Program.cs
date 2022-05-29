using gRPC.Server;
using gRPC.Server.Extensions;
using gRPC.Server.Services;
using gRPC.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddGrpc();

builder.Services
    .AddSingleton<IProduce, Produce>();

builder.Services.AddHostedService<KafkaBackgroundService>();

builder.Services.AddRelationalDatabase(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ServerContext>();
    db.Database.Migrate();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", context => context.Response.WriteAsync("Starting gRPC Demo.\n Look at the console output for details ...\n"));
    endpoints.MapGrpcService<GRpcService>();
});

app.Run();