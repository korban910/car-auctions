using AuctionService.Consumers;
using MassTransit;
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

    public static void AddMassTransitSerivces(this IServiceCollection services)
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
            config.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("auction", false));
    
            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.ConfigureEndpoints(ctx);
            });
        });
    }
}