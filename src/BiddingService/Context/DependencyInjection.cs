using BiddingService.Consumers;
using BiddingService.services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BiddingService.Context;

public static class DependencyInjection
{
    public static void AddMassTransitServices(this IServiceCollection services)
    {
        services.AddMassTransit(config =>
        {
            config.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();
            config.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(
                Environment.GetEnvironmentVariable("BID_MASS_PREFIX")!, 
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
    
    public static void AddTokenVerification(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = Environment.GetEnvironmentVariable("IDENTITY_URL");
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters.ValidateAudience = false;
                options.TokenValidationParameters.NameClaimType = Environment.GetEnvironmentVariable("CLAIM_USER_NAME");
            });
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddHostedService<CheckAuctionFinished>();
    }
}