using MongoDB.Entities;

namespace BiddingService.Entities;

public class Auction : Entity
{
    public int ReservePrice { get; set; }
    public required string Seller { get; set; }
    public DateTimeOffset AuctionEnd { get; set; }
    public bool Finished { get; set; }
}