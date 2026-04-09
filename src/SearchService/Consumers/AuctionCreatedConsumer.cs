using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Entities;

namespace SearchService.Consumers;

public class AuctionCreatedConsumer(IMapper mapper) : IConsumer<AuctionCreated>
{
    public async Task Consume(ConsumeContext<AuctionCreated> context)
    {
        Console.WriteLine($"--> Consuming auction created: {context.Message.Id}");
        
        var item = mapper.Map<Auction>(context.Message);

        // For error testing
        if (item.Item.Model == "Foo")
        {
            throw new ArgumentException("Cannot sell cars with name of Foo");
        }

        await item.SaveAsync();
    }
}