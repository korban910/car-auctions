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
        }
    ];
}