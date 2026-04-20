using Duende.IdentityServer.Models;

namespace IdentityService;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
    [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
    ];

    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new(Environment.GetEnvironmentVariable("APP_SCOPE")!, Environment.GetEnvironmentVariable("DISPLAY_NAME")!)
    ];

    public static IEnumerable<Client> Clients =>
    [
        new ()
        {
            ClientId = Environment.GetEnvironmentVariable("POSTMAN_CLIENT_ID")!,
            ClientName = Environment.GetEnvironmentVariable("POSTMAN_CLIENT_NAME")!,
            AllowedScopes =
            {
                Environment.GetEnvironmentVariable("OPEN_ID_SCOPE")!,
                Environment.GetEnvironmentVariable("PROFILE_SCOPE")!,
                Environment.GetEnvironmentVariable("APP_SCOPE")!
            },
            RedirectUris = { Environment.GetEnvironmentVariable("POSTMAN_REDIRECTURI")! },
            ClientSecrets = [
                new Secret(Environment.GetEnvironmentVariable("POSTMAN_SECRET")!.Sha256()) 
            ],
            AllowedGrantTypes = { GrantType.ResourceOwnerPassword },
        },
        new ()
        {
            ClientId = Environment.GetEnvironmentVariable("NEXT_APP_ID")!,
            ClientName = Environment.GetEnvironmentVariable("NEXT_CLIENT_NAME")!,
            ClientSecrets = [
                new Secret(Environment.GetEnvironmentVariable("NEXT_SECRET")!.Sha256())
            ],
            AllowedGrantTypes = { GrantType.ClientCredentials, GrantType.AuthorizationCode },
            RequirePkce = false,
            RedirectUris = { Environment.GetEnvironmentVariable("NEXT_REDIRECTURI")! }, 
            AllowOfflineAccess = true,
            AllowedScopes =
            {
                Environment.GetEnvironmentVariable("OPEN_ID_SCOPE")!,
                Environment.GetEnvironmentVariable("PROFILE_SCOPE")!,
                Environment.GetEnvironmentVariable("APP_SCOPE")!
            },
            AccessTokenLifetime = 3600 * 24 * 30
        }
    ];
}