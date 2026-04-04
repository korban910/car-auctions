using MongoDB.Entities;

namespace SearchService.Entities;

public class Auction : Entity
{
    public int ReservePrice { get; set; }
    public string? Seller { get; set; }
    public string? Winner  { get; set; }
    public int SoldAmount { get; set; }
    public int CurrentHighBid { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset AuctionEnd { get; set; }
    public required string Status { get; set; }
    public Item Item { get; set; }
}