using AuctionService.Common.Seeds;
using AuctionService.Context;
using AuctionService.Mapping;
using Scalar.AspNetCore; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuctionDbContext();
builder.Services.AddMapping();
builder.Services.AddMassTransitSerivces();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthorization();

app.MapControllers();

try
{
    app.InitDb();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

app.Run();
