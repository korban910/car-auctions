using System.Net;
using MongoDB.Entities;
using Polly;
using Polly.Extensions.Http;
using SearchService.Entities;

namespace SearchService.Common.Services;

public class AuctionServiceHttpClient(HttpClient httpClient)
{
    public async Task<List<Auction>> GetItemsForSearchDb()
    {
        var lastUpdated = await DB.Find<Auction, DateTimeOffset?>()
            .Sort(x => x.Descending(a => a.UpdatedAt))
            .Project(x => x.UpdatedAt)
            .ExecuteFirstAsync();

        var auctionUrl = Environment.GetEnvironmentVariable("AUCTION_URL")!;

        var dateParam = lastUpdated?.ToUniversalTime().ToString("o") ??
                        DateTimeOffset.MinValue.ToUniversalTime().ToString("o");

        var encodedDate = Uri.EscapeDataString(dateParam);

        var response = await httpClient.GetFromJsonAsync<List<Auction>>(
            $"{auctionUrl}/api/auctions?date={encodedDate}");

        return response ?? [];
    }

    public static IAsyncPolicy<HttpResponseMessage> GetPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
            .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(3));
    }
}