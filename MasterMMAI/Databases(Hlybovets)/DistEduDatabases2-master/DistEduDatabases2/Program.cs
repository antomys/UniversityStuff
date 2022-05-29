using System.Text.Json.Serialization;
using Bogus;
using DistEduDatabases2.Mongo.Extensions;
using DistEduDatabases2.Neo4j;
using DistEduDatabases2.Neo4j.Extensions;
using DistEduDatabases2.Relational.Context;
using DistEduDatabases2.Relational.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Randomizer.Seed = Random.Shared;

builder.Services
    .AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services
    .AddRelationalDatabase(builder.Configuration)
    .AddDocumentedDatabase(builder.Configuration)
    .AddNeo4JDatabase(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ServerContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();