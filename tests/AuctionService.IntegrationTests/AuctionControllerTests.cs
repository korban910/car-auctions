using System.Net;
using System.Net.Http.Json;
using AuctionService.Context;
using AuctionService.DTOs;
using AuctionService.IntegrationTests.Fixtures;
using AuctionService.IntegrationTests.Util;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionService.IntegrationTests;

public class AuctionControllerTests(CustomWebAppFactory factory) : IClassFixture<CustomWebAppFactory>, IAsyncLifetime
{
    private readonly HttpClient _httpClient = factory.CreateClient();
    private const string GT_ID = "155225c1-4448-4066-9886-6786536e05ea";

    [Fact]
    public async Task GetAuctions_ShouldReturn4Auctions()
    {
        // arrange
        
        // act
        var response = await _httpClient.GetFromJsonAsync<List<AuctionDto>>("/api/auctions");
        
        // assert
        Assert.Equal(4, response?.Count);
    }

    [Fact]
    public async Task GetAuctionById_WithValidId_ShouldReturnAuction()
    {
        // arrange
        
        // act
        var response = await _httpClient.GetFromJsonAsync<AuctionDto>($"/api/auctions/{GT_ID}");
        
        // assert
        Assert.Equal("SLK", response?.Item.Model);
    }
    
    [Fact]
    public async Task GetAuctionById_WithInValidId_ShouldReturn404()
    {
        // arrange
        
        // act
        var response = await _httpClient.GetAsync($"/api/auctions/{Guid.NewGuid()}");
        
        // assert
        Assert.Equal(HttpStatusCode.NotFound, response?.StatusCode);
    }
    
    [Fact]
    public async Task GetAuctionById_WithInValidGuidId_ShouldReturn400()
    {
        // arrange
        
        // act
        var response = await _httpClient.GetAsync("/api/auctions/not-a-guid");
        
        // assert
        Assert.Equal(HttpStatusCode.BadRequest, response?.StatusCode);
    }

    public Task InitializeAsync()
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

    public Task DisposeAsync() => Task.CompletedTask;
}