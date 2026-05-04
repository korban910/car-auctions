using System.Net;
using System.Net.Http.Json;
using AuctionService.Context;
using AuctionService.DTOs;
using AuctionService.IntegrationTests.Constants;
using AuctionService.IntegrationTests.Fixtures;
using AuctionService.IntegrationTests.Util;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionService.IntegrationTests;

[Collection(TestConstants.SharedCollection)]
public class AuctionControllerTests(CustomWebAppFactory factory) : IAsyncLifetime
{
    private readonly HttpClient _httpClient = factory.CreateClient();
    private const string SlkId = "155225c1-4448-4066-9886-6786536e05ea";

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
        var response = await _httpClient.GetFromJsonAsync<AuctionDto>($"/api/auctions/{SlkId}");
        
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

    [Fact]
    public async Task CreateAuction_WithNoAuth_ShouldReturn401()
    {
        // arrange
        var auction = GetAuctionForCreate();
        
        // act
        var response = await _httpClient.PostAsJsonAsync("/api/auctions", auction);
        
        // assert
        Assert.Equal(HttpStatusCode.Unauthorized, response?.StatusCode);
    }
    
    [Fact]
    public async Task CreateAuction_WithAuth_ShouldReturn201()
    {
        // arrange
        var auction = GetAuctionForCreate();
        _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("bob"));
        
        // act
        var response = await _httpClient.PostAsJsonAsync("/api/auctions", auction);
        
        // assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response?.StatusCode);
    }
    
    [Fact]
    public async Task CreateAuction_WithAuth_ShouldReturnBobAsSeller()
    {
        // arrange
        var auction = GetAuctionForCreate();
        const string userName = "Bob";
        _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser(userName));
        
        // act
        var response = await _httpClient.PostAsJsonAsync("/api/auctions", auction);
        
        // assert
        response.EnsureSuccessStatusCode();
        var createdAuction = await response.Content.ReadFromJsonAsync<AuctionDto>();
        Assert.Equal(userName, createdAuction?.Seller);
    }
    
    [Fact]
    public async Task CreateAuction_WithInvalidCreateAuctionDto_ShouldReturn400()
    {
        // arrange
        var auction = GetAuctionForCreate();
        auction.Make = null;
        const string userName = "Bob";
        _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser(userName));

        // act
        var response = await _httpClient.PostAsJsonAsync("/api/auctions", auction);

        // assert
        Assert.Equal(HttpStatusCode.BadRequest, response?.StatusCode);
    }

    [Fact]
    public async Task UpdateAuction_WithValidUpdateDtoAndUser_ShouldReturn200()
    {
        // arrange
        var updateAuctionDto = new UpdateAuctionDto()
        {
            Make = "BMW",
            Model = "X3",
            Year = 2017,
            Color = "White",
            Mileage = 45000,
        };
        const string userName = "tom";
        _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser(userName));

        // act
        var response =  await _httpClient.PutAsJsonAsync($"/api/auctions/{SlkId}", updateAuctionDto);

        // assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response?.StatusCode);
    }

    [Fact]
    public async Task UpdateAuction_WithValidUpdateDtoAndInvalidUser_ShouldReturn403()
    {
        // arrange
        var updateAuctionDto = new UpdateAuctionDto()
        {
            Make = "BMW",
            Model = "X3",
            Year = 2017,
            Color = "White",
            Mileage = 45000,
        };
        const string userName = "test";
        _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser(userName));

        // act
        var response =  await _httpClient.PutAsJsonAsync($"/api/auctions/{SlkId}", updateAuctionDto);

        // assert
        Assert.Equal(HttpStatusCode.Forbidden, response?.StatusCode);
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