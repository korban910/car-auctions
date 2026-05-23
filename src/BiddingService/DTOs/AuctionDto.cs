namespace BiddingService.DTOs;

public class AuctionDto
{
    public int ReservePrice { get; set; }
    public required string Seller { get; set; }
    public DateTimeOffset AuctionEnd { get; set; }
    public bool Finished { get; set; }
}