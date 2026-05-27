using System.Net;
using AuctionService.Consumers;
using AuctionService.Context.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Context;

public static class DependencyInjection
{
    public static void AddAuctionDbContext(this IServiceCollection services)
    {
        services.AddDbContext<AuctionDbContext>(options =>
        {
            options.UseNpgsql(Environment.GetEnvironmentVariable("AUCTION_DATABASE"));
        });
    }

    public static void AddMassTransitServices(this IServiceCollection services)
    {
        services.AddMassTransit(config =>
        {
            config.AddEntityFrameworkOutbox<AuctionDbContext>(o =>
            {
                o.QueryDelay = TimeSpan.FromSeconds(10);
                o.UsePostgres();
                o.UseBusOutbox();
            });
            
            config.AddConsumersFromNamespaceContaining<AuctionCreatedFaultConsumer>();
            config.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(
                Environment.GetEnvironmentVariable("AUCTION_MASS_PREFIX")!,
                false));
    
            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(Environment.GetEnvironmentVariable("RABBITMQ_HOST"), "/", host =>
                {
                    host.Username(Environment.GetEnvironmentVariable("RABBITMQ_USER")!);
                    host.Password(Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD")!);
                });
                cfg.ConfigureEndpoints(ctx);
            });
        });
    }

    public static void AddDependencyInjections(this IServiceCollection services)
    {
        services.AddScoped<IAuctionRepository, AuctionRepository>();
    }

    public static void AddKestrelServices(this IServiceCollection services)
    {
        services.Configure<KestrelServerOptions>(options =>
        {
            options.ListenAnyIP(int.Parse(Environment.GetEnvironmentVariable("GRPC_PORT")!),
                listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http2;
                });
            
            options.ListenAnyIP(int.Parse(Environment.GetEnvironmentVariable("WEB_API_PORT")!),
                listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http1;
                });
        });
    }
}