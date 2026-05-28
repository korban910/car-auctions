using Microsoft.AspNetCore.Authentication.JwtBearer;
using Yarp.ReverseProxy.Configuration;

namespace GatewayService;

public static class DependencyInjection
{
    public static void AddYarp(this IServiceCollection services)
    {
        var routes = GetRouteConfig();
        var clusters = GetClusterConfig();

        services.AddReverseProxy()
            .LoadFromMemory(routes, clusters);
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

    private static RouteConfig[] GetRouteConfig()
    {
        return
        [
            new RouteConfig
            {
                RouteId = Environment.GetEnvironmentVariable("AUCTION_CLUSTER_READ")!,
                ClusterId = Environment.GetEnvironmentVariable("AUCTION_CLUSTER_ID"),
                Match = new RouteMatch
                {
                    Path = "/auctions/{**catch-all}",
                    Methods = ["GET"],
                },
                Transforms =
                [
                    new Dictionary<string, string> 
                    { 
                        { "PathPattern", "api/auctions/{**catch-all}" } 
                    }
                ]
            },
            new RouteConfig
            {
                RouteId = Environment.GetEnvironmentVariable("AUCTION_CLUSTER_WRITE")!,
                ClusterId = Environment.GetEnvironmentVariable("AUCTION_CLUSTER_ID"),
                AuthorizationPolicy = Environment.GetEnvironmentVariable("AUTHORIZATION_POLICY")!,
                Match = new RouteMatch
                {
                    Path= "/auctions/{**catch-all}",
                    Methods = ["POST", "PUT", "DELETE"],
                },
                Transforms =
                [
                    new Dictionary<string, string> 
                    { 
                        { "PathPattern", "api/auctions/{**catch-all}" } 
                    }
                ]
            },
            new RouteConfig()
            {
                RouteId = Environment.GetEnvironmentVariable("SEARCH_CLUSTER_READ")!,
                ClusterId = Environment.GetEnvironmentVariable("SEARCH_CLUSTER_ID"),
                Match = new RouteMatch()
                {
                    Path = "/search/{**catch-all}",
                    Methods = ["GET"],
                },
                Transforms = [
                    new Dictionary<string, string>()
                    {
                        { "PathPattern", "api/search/{**catch-all}" }
                    }
                ]
            },
            new RouteConfig()
            {
                RouteId = Environment.GetEnvironmentVariable("BIDS_CLUSTER_READ")!,
                ClusterId = Environment.GetEnvironmentVariable("BIDS_CLUSTER_ID"),
                Match = new RouteMatch()
                {
                    Path = "/bids/{**catch-all}",
                    Methods = ["GET"],
                },
                Transforms = [
                    new Dictionary<string, string>()
                    {
                        { "PathPattern", "api/bids/{**catch-all}" }
                    }
                ]
            },
            new RouteConfig()
            {
                RouteId = Environment.GetEnvironmentVariable("BIDS_CLUSTER_WRITE")!,
                ClusterId = Environment.GetEnvironmentVariable("BIDS_CLUSTER_ID"),
                AuthorizationPolicy = Environment.GetEnvironmentVariable("AUTHORIZATION_POLICY")!,
                Match = new RouteMatch()
                {
                    Path = "/bids",
                    Methods = ["POST"],
                },
                Transforms = [
                    new Dictionary<string, string>()
                    {
                        { "PathPattern", "api/bids" }
                    }
                ]
            },
            new RouteConfig()
            {
                RouteId = Environment.GetEnvironmentVariable("NOTIFICATION_CLUSTER")!,
                ClusterId = Environment.GetEnvironmentVariable("NOTIFICATION_CLUSTER_ID"),
                Match = new RouteMatch()
                {
                    Path = "/notifications/{**catch-all}",
                }
            }
        ];
    }

    private static ClusterConfig[] GetClusterConfig()
    {
        return
        [
            new ClusterConfig()
            {
                ClusterId = Environment.GetEnvironmentVariable("AUCTION_CLUSTER_ID")!,
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    {
                        Environment.GetEnvironmentVariable("AUCTION_API")!,
                        new DestinationConfig { Address = Environment.GetEnvironmentVariable("AUCTION_URL")! }
                    }
                }
            },
            new ClusterConfig()
            {
                ClusterId = Environment.GetEnvironmentVariable("SEARCH_CLUSTER_ID")!,
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    {
                        Environment.GetEnvironmentVariable("SEARCH_API")!,
                        new DestinationConfig { Address = Environment.GetEnvironmentVariable("SEARCH_URL")! }
                    }
                }
            },
            new ClusterConfig()
            {
                ClusterId = Environment.GetEnvironmentVariable("BIDS_CLUSTER_ID")!,
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    {
                        Environment.GetEnvironmentVariable("BIDS_API")!,
                        new DestinationConfig
                        {
                            Address = Environment.GetEnvironmentVariable("BIDS_URL")!
                        }
                    }
                }
            },
            new ClusterConfig()
            {
                ClusterId = Environment.GetEnvironmentVariable("NOTIFICATION_CLUSTER_ID")!,
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    {
                        Environment.GetEnvironmentVariable("NOTIFICATION_API")!,
                        new DestinationConfig
                        {
                            Address = Environment.GetEnvironmentVariable("NOTIFICATION_URL")!
                        }
                    }
                }
            }
        ];
    }
}