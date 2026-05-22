using BiddingService.Common.Enums;
using MongoDB.Entities;

namespace BiddingService.Entities;

public class Bid : Entity
{
    public required string AuctionId { get; set; }
    public required string Bidder { get; set; }
    public DateTimeOffset BidTime { get; set; } = DateTimeOffset.UtcNow;
    public BidStatus BidStatus { get; set; } = BidStatus.Unknown;
    public int Amount { get; set; }
}