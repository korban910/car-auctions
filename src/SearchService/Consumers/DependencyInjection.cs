using MassTransit;

namespace SearchService.Consumers;

public static class DependencyInjection
{
    public static void AddMassTransientServices(this IServiceCollection services)
    {
        services.AddMassTransit(config =>
        {
            config.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();
            
            config.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(
                Environment.GetEnvironmentVariable("SEARCH_MASS_PREFIX")!, 
                false));
            
            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(Environment.GetEnvironmentVariable("RABBITMQ_HOST"), "/", host =>
                {
                    host.Username(Environment.GetEnvironmentVariable("RABBITMQ_USER")!);
                    host.Password(Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD")!);
                });
                
                cfg.ReceiveEndpoint("search-auction-created", e =>
                {
                    e.UseMessageRetry(r => r.Interval(5, 5));
                    e.ConfigureConsumer<AuctionCreatedConsumer>(ctx);
                });
                
                cfg.ConfigureEndpoints(ctx);
            });
        });
    }
}