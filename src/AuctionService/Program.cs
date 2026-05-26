using AuctionService.Common.Seeds;
using AuctionService.Context;
using AuctionService.Mapping;
using AuctionService.Services;
using Scalar.AspNetCore; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddKestrelServices();
builder.Services.AddAuctionDbContext();
builder.Services.AddMapping();
builder.Services.AddMassTransitServices();
builder.Services.AddDependencyInjections();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddTokenVerification();
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<GrpcAuctionService>();

if (!app.Environment.IsEnvironment(Environment.GetEnvironmentVariable("INTEGRATION_TEST")!))
{
    try
    {
        app.InitDb();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}
app.Run();
