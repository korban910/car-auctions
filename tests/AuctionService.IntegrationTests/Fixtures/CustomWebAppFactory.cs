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
using WebMotions.Fake.Authentication.JwtBearer;

namespace AuctionService.IntegrationTests.Fixtures;

public class CustomWebAppFactory: WebApplicationFactory<Program>, IAsyncLifetime
{
    private PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder().Build();

    public CustomWebAppFactory()
    {
        DotNetEnv.Env.Load();
    }

    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(Environment.GetEnvironmentVariable("INTEGRATION_TEST")!);
        
        builder.ConfigureTestServices(services =>
        {
            services.RemoveDbContext();

            services.AddDbContext<AuctionDbContext>(options =>
            {
                options.UseNpgsql(_postgreSqlContainer.GetConnectionString());
            });

            services.AddMassTransitTestHarness();
            
            services.EnsureCreated();

            services.AddAuthentication(FakeJwtBearerDefaults.AuthenticationScheme)
                .AddFakeJwtBearer(opt =>
                {
                    opt.BearerValueType = FakeJwtBearerBearerValueType.Jwt;
                });
        });
        
        base.ConfigureWebHost(builder);
    }

    Task IAsyncLifetime.DisposeAsync() => _postgreSqlContainer.DisposeAsync().AsTask();
}