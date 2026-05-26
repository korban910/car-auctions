namespace BiddingService.DTOs;

public record PlaceBidRequest(
    string AuctionId,
    int Amount);