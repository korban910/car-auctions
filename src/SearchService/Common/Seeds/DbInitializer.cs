using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Entities;

namespace SearchService.Common.Seeds;

public class DbInitializer
{
    public static async Task InitDb(WebApplication app)
    {
        await DB.InitAsync(Environment.GetEnvironmentVariable("SEARCH_DATABASE_NAME")!, 
            MongoClientSettings.FromConnectionString(Environment.GetEnvironmentVariable("SEARCH_DATABASE")));

        await DB.Index<Auction>()
            .Key(x => x.Item.Make, KeyType.Text)
            .Key(x => x.Item.Model, KeyType.Text)
            .Key(x => x.Item.Color, KeyType.Text)
            .CreateAsync();
        
        var count = await DB.CountAsync<Auction>();

        if (count == 0)
        {
            Console.WriteLine("No data - will attempt to seed");
            var auctionData = await File.ReadAllTextAsync("Common/Seeds/auctions.json");
            
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive =  true };
            var auctions = JsonSerializer.Deserialize<List<Auction>>(auctionData, options);
            
            await DB.SaveAsync(auctions);
        }
    }
}