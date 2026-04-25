using AuctionService.Common.Enums;

namespace AuctionService.Entities;

public class Auction
{
    public Guid Id { get; set; }
    public int ReservePrice { get; set; }
    public string? Seller { get; set; }
    public string? Winner  { get; set; }
    public int? SoldAmount { get; set; }
    public int? CurrentHighBid { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset AuctionEnd { get; set; }
    public Status Status { get; set; } = Status.Unknown;
    public Item Item { get; set; }
    public bool HasReservePrice() => ReservePrice > 0;
}