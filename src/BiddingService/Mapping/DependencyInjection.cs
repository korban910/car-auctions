using BiddingService.Common.Serializers;
using MongoDB.Bson.Serialization;

namespace BiddingService.Mapping;

public static class DependencyInjection
{
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        BsonSerializer.RegisterSerializer(new BidStatusSerializer());

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        return services;
    }
}