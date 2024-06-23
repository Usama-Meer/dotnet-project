using GameStore.Data;
using GameStore.Endpoints;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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



var app = builder.Build();






//creating map group route

app.mapRouteEndpoints();


//creating database migrations
await app.MigrateDbAsync();

app.Run();

