using AuctionService;
using BiddingService.Entities;
using Grpc.Net.Client;

namespace BiddingService.services;

public class GrpcAuctionClient(
    ILogger<GrpcAuctionClient> logger)
{
    public Auction? GetAuction(string id)
    {
        logger.LogInformation("Calling GRPC Service");
        var channel = GrpcChannel.ForAddress(Environment.GetEnvironmentVariable("GRPC_URL")!);
        var client = new GrpcAuction.GrpcAuctionClient(channel);
        var request = new GetAuctionRequest { Id = id };

        try
        {
            var reply = client.GetAuction(request);
            var auction = new Auction()
            {
                ID = reply.Auction.Id,
                Seller = reply.Auction.Seller,
                AuctionEnd = DateTimeOffset.Parse(reply.Auction.AuctionEnd),
                ReservePrice = reply.Auction.ReservePrice,
            };

            return auction;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Could not call GRPC Server");
            return null;
        }
    }
}