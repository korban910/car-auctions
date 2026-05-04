using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Testcontainers.MongoDb;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Entities;

namespace SearchService.IntegrationTests.Fixtures;

public class CustomWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MongoDbContainer _mongoContainer = new MongoDbBuilder()
        .WithImage("mongo:latest")
        .Build();
    
    public async Task InitializeAsync()
    {
        DotNetEnv.Env.Load();
        await _mongoContainer.StartAsync();

        var connString = _mongoContainer.GetConnectionString();

        await DB.InitAsync(
            Environment.GetEnvironmentVariable("TEST_MONGO_DB")!, 
            MongoClientSettings.FromConnectionString(connString));

        await DB.Index<Auction>()
            .Key(x => x.Item.Make, KeyType.Text)
            .Key(x => x.Item.Model, KeyType.Text)
            .Key(x => x.Item.Color, KeyType.Text)
            .CreateAsync();
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(Environment.GetEnvironmentVariable("TESTING")!);
        
        builder.ConfigureTestServices(services =>
        {
            services.AddMassTransitTestHarness();
        });
    }

    public new async Task DisposeAsync() => await _mongoContainer.StopAsync();
} 