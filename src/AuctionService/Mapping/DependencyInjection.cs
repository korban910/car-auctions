using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AuctionService.Mapping;

public static class DependencyInjection
{
    public static void AddMapping(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { }, typeof(DependencyInjection).Assembly);
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
}