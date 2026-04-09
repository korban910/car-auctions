using MassTransit;

namespace SearchService.Consumers;

public static class DependencyInjection
{
    public static void AddMassTransientServices(this IServiceCollection services)
    {
        services.AddMassTransit(config =>
        {
            config.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();
            
            config.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));
            
            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.ReceiveEndpoint("search-auction-created", e =>
                {
                    e.UseMessageRetry(r => r.Interval(5, 5));
                    e.ConfigureConsumer<AuctionCreatedConsumer>(ctx);
                });
                
                cfg.ReceiveEndpoint("search-auction-updated", e =>
                {
                    e.UseMessageRetry(r => r.Interval(5, 5));
                    e.ConfigureConsumer<AuctionUpdatedConsumer>(ctx);
                });
                
                cfg.ReceiveEndpoint("search-auction-deleted", e =>
                {
                    e.UseMessageRetry(r => r.Interval(5, 5));
                    e.ConfigureConsumer<AuctionDeletedConsumer>(ctx);
                });
                
                cfg.ConfigureEndpoints(ctx);
            });
        });
    }
}