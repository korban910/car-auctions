using Scalar.AspNetCore;
using SearchService.Common.Seeds;
using SearchService.Common.Services;
using SearchService.Consumers;
using SearchService.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services
    .AddHttpClient<AuctionServiceCHttpClient>()
    .AddPolicyHandler(AuctionServiceCHttpClient.GetPolicy());

builder.Services.AddMapper();

builder.Services.AddTransientServices();

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

app.Lifetime.ApplicationStarted.Register(async void () =>
{
    try
    {
        await app.InitDb();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
});

app.Run();