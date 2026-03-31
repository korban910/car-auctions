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
}