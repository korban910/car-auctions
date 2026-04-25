using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Context.Interfaces;

public class AuctionRepository(
    AuctionDbContext context,
    IMapper mapper): IAuctionRepository
{
    public async Task<List<AuctionDto>> GetAuctionsAsync(string? date)
    {
        var query = context.Auctions.OrderBy(x => x.Item.Make).AsQueryable();

        if (!string.IsNullOrEmpty(date))
        {
            //query = query.Where(x => x.UpdatedAt.CompareTo(DateTimeOffset.Parse(date).ToUniversalTime()) > 0);
            if (DateTimeOffset.TryParse(date, out var parsedDate))
            {
                var utcDate = parsedDate.ToUniversalTime();
                query = query.Where(x => x.UpdatedAt > utcDate);
            }
        }
        
        return await query.ProjectTo<AuctionDto>(mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<AuctionDto?> GetAuctionByIdAsync(Guid id)
    {
        return await context.Auctions
            .ProjectTo<AuctionDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Auction?> GetAuctionEntityById(Guid id)
    {
        return await context.Auctions
            .Include(x => x.Item)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task AddAuctionAsync(Auction auction)
    {
        await context.Auctions.AddAsync(auction);
    }

    public void RemoveAuction(Auction auction)
    {
        context.Auctions.Remove(auction);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }
}