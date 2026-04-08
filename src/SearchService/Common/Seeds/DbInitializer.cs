using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Common.Services;
using SearchService.Entities;

namespace SearchService.Common.Seeds;

public static class DbInitializer
{
    public static async Task InitDb(this WebApplication app)
    {
        await DB.InitAsync(Environment.GetEnvironmentVariable("SEARCH_DATABASE_NAME")!,
            MongoClientSettings.FromConnectionString(Environment.GetEnvironmentVariable("SEARCH_DATABASE")));

        await DB.Index<Auction>()
            .Key(x => x.Item.Make, KeyType.Text)
            .Key(x => x.Item.Model, KeyType.Text)
            .Key(x => x.Item.Color, KeyType.Text)
            .CreateAsync();

        var count = await DB.CountAsync<Auction>();

        using var scope = app.Services.CreateScope();
        var httpClient = scope.ServiceProvider.GetRequiredService<AuctionServiceHttpClient>();

        var items = await httpClient.GetItemsForSearchDb();

        Console.WriteLine(items.Count + " returned from the auction service");

        if (items.Count > 0) await DB.SaveAsync(items);

        // Reading from auctions.json
        /*if (count == 0)
        {
            Console.WriteLine("No data - will attempt to seed");
            var auctionData = await File.ReadAllTextAsync("Common/Seeds/auctions.json");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive =  true };
            var auctions = JsonSerializer.Deserialize<List<Auction>>(auctionData, options);

            await DB.SaveAsync(auctions);
        }*/
    }
}