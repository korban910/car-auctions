using AuctionService.Context;
using Grpc.Core;

namespace AuctionService.Services;

public class GrpcAuctionService(AuctionDbContext dbContext) : GrpcAuction.GrpcAuctionBase
{
    public override async Task<GrpcAuctionResponse> GetAuction(
        GetAuctionRequest request, 
        ServerCallContext context)
    {
        Console.WriteLine("==> Received Grpc request for auction");

        if (await dbContext.Auctions.FindAsync(Guid.Parse(request.Id)) is not { } auction)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Could not find auction"));
        }

        var response = new GrpcAuctionResponse()
        {
            Auction = new GrpcAuctionModel()
            {
                Id = auction.Id.ToString(),
                Seller = auction.Seller,
                AuctionEnd = auction.AuctionEnd.ToString(),
                ReservePrice = auction.ReservePrice,
            }
        };
            
        return response;
    }
}