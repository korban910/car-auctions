using System.Net;
using System.Net.Http.Json;
using AuctionService.Context;
using AuctionService.DTOs;
using AuctionService.IntegrationTests.Constants;
using AuctionService.IntegrationTests.Fixtures;
using AuctionService.IntegrationTests.Util;
using Contracts;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionService.IntegrationTests;

[Collection(TestConstants.SharedCollection)]
public class AuctionBusTests(CustomWebAppFactory factory) : IAsyncLifetime
{
    private readonly HttpClient _httpClient = factory.CreateClient();
    private const string SlkId = "155225c1-4448-4066-9886-6786536e05ea";
    private readonly ITestHarness _testHarness = factory.Services.GetTestHarness();

    [Fact]
    public async Task CreateAuction_WithValidObject_ShouldPublishAuctionCreated()
    {
        // arrange
        var auction = GetAuctionForCreate();
        _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("bob"));
        
        // act
        var response = await _httpClient.PostAsJsonAsync("/api/auctions", auction);
        
        // assert
        response.EnsureSuccessStatusCode();
        Assert.True(await _testHarness.Published.Any<AuctionCreated>());
    }
    
    public Task InitializeAsync() => Task.CompletedTask;
    
    public Task DisposeAsync()
    {
        try
        {
            using var scope = factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AuctionDbContext>();
        
            DbHelper.ReinitDbForTests(db);
            return Task.CompletedTask;
        }
        catch (Exception exception)
        {
            return Task.FromException(exception);
        }
    }
    
    private static CreateAuctionDto GetAuctionForCreate()
    {
        return new CreateAuctionDto
        {
            Make = "test",
            Model = "testModel",
            Year = 2026,
            Color = "test",
            Mileage = 10,
            ImageUrl = "test",
            ReservePrice = 10,
            AuctionEnd = DateTimeOffset.UtcNow
        };
    }
}