using GameStore.Data;
using GameStore.Endpoints;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using GameStore.Mapping;

var builder = WebApplication.CreateBuilder(args);


//first method but not secure 
// var connString="Data Source=GameStore.db";
// builder.Services.AddSqlite<GameStoreContext>(connString);

// another method
//add "ConnectionStrings":{"GameStore":"Data-Source=GameStore.db"}
//string for creation of instance of database
var connString=builder.Configuration.GetConnectionString("GameStore");

//create database

builder.Services.AddSqlite<GameStoreContext>(connString);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(gameMapping));


var app = builder.Build();






//creating map group route

app.mapRouteEndpoints();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

//creating database migrations
await app.MigrateDbAsync();

app.Run();

