using MassTransit;
using NotificationService.Consumers;

namespace NotificationService.Context;

public static class DependencyInjection
{
    public static void AddMassTransitServices(this IServiceCollection services)
    {
        services.AddMassTransit(config =>
        {
            config.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();
                
            config.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(
                Environment.GetEnvironmentVariable("NOTIFICATION_MASS_PREFIX")!, 
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
}