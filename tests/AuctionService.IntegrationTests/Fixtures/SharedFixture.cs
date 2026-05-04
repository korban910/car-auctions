using AuctionService.IntegrationTests.Constants;

namespace AuctionService.IntegrationTests.Fixtures;

[CollectionDefinition(TestConstants.SharedCollection)]
public class SharedFixture : ICollectionFixture<CustomWebAppFactory>
{
    
}