using BiddingService.Context;
using BiddingService.Mapping;
using BiddingService.services;
using MongoDB.Driver;
using MongoDB.Entities;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransitServices();
builder.Services.AddTokenVerification();
builder.Services.AddMapping();
builder.Services.AddServices();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<GrpcAuctionClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

await Policy.Handle<TimeoutException>()
    .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(5))
    .ExecuteAndCaptureAsync(async () =>
    {
        await DB.InitAsync(Environment.GetEnvironmentVariable("BIDDING_DATABASE_NAME")!, 
            MongoClientSettings.FromConnectionString(Environment.GetEnvironmentVariable("BIDDING_DATABASE")!));
    });

app.Run();
