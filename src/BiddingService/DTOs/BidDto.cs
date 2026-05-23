using BiddingService.Common.Enums;

namespace BiddingService.DTOs;

public class BidDto
{
    public required string Id { get; set; }
    public required Guid AuctionId { get; set; }
    public required string Bidder { get; set; }
    public DateTimeOffset BidTime { get; set; }
    public required string BidStatus { get; set; }
    public required string BidStatusHtml { get; set; }
    
    public int Amount { get; set; }
}