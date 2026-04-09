using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Entities;

namespace SearchService.Consumers;

public class AuctionUpdatedConsumer(IMapper mapper) : IConsumer<AuctionUpdated>
{
    public async Task Consume(ConsumeContext<AuctionUpdated> context)
    {
        Console.WriteLine($"--> Consuming auction updated: {context.Message.Id}");
        
        var item =  mapper.Map<Auction>(context.Message);
        
        await DB.Update<Auction>()
            .MatchID(item.ID)
            .Modify(a => a.Item.Make, context.Message.Make)
            .Modify(a => a.Item.Model, context.Message.Model)
            .Modify(a => a.Item.Color, context.Message.Color)
            .Modify(a => a.Item.Mileage, context.Message.Mileage)
            .Modify(a => a.Item.Year, context.Message.Year)
            .ExecuteAsync();
    }
}