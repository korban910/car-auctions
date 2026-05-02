using AuctionService.Context;
using AuctionService.IntegrationTests.Util;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace AuctionService.IntegrationTests.Fixtures;

public class CustomWebAppFactory: WebApplicationFactory<Program>, IAsyncLifetime
{
    private PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder().Build();

    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("INTEGRATION_TEST", "IntegrationTest");
        Environment.SetEnvironmentVariable("IDENTITY_URL", "http://localhost:5000");
        Environment.SetEnvironmentVariable("CLAIM_USER_NAME", "username");
        
        builder.UseEnvironment(Environment.GetEnvironmentVariable("INTEGRATION_TEST"));
        
        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "IDENTITY_URL", Environment.GetEnvironmentVariable("IDENTITY_URL") },
                { "CLAIM_USER_NAME", Environment.GetEnvironmentVariable("CLAIM_USER_NAME") }
            });
        });
        
        builder.ConfigureTestServices(services =>
        {
            services.RemoveDbContext();

            services.AddDbContext<AuctionDbContext>(options =>
            {
                options.UseNpgsql(_postgreSqlContainer.GetConnectionString());
            });

            services.AddMassTransitTestHarness();
            
            services.EnsureCreated();
        });
        
        base.ConfigureWebHost(builder);
    }

    Task IAsyncLifetime.DisposeAsync() => _postgreSqlContainer.DisposeAsync().AsTask();
}