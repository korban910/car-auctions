using BiddingService.Context;
using BiddingService.Mapping;
using MongoDB.Driver;
using MongoDB.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransitServices();
builder.Services.AddTokenVerification();
builder.Services.AddMapping();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

await DB.InitAsync(Environment.GetEnvironmentVariable("BIDDING_DATABASE_NAME")!, 
    MongoClientSettings.FromConnectionString(Environment.GetEnvironmentVariable("BIDDING_DATABASE")!));

app.Run();
