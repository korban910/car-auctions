using AuctionService.Context;
using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers;

[ApiController]
[Route("api/auctions")]
public class AuctionsController(
    AuctionDbContext context, 
    IMapper mapper,
    IPublishEndpoint publishEndpoint) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions(string? date)
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

    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
    {
        if (await context.Auctions.Include(x => x.Item)
                .FirstOrDefaultAsync(x => x.Id == id) is not { } auction)
        {
            return NotFound();
        }
        
        return mapper.Map<AuctionDto>(auction);
    }

    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto createAuctionDto)
    {
        var auction = mapper.Map<Auction>(createAuctionDto);
        
        // TODO: add current user as seller
        auction.Seller = "Test";
        
        await context.AddAsync(auction);
        
        var auctionDto= mapper.Map<AuctionDto>(auction);
        var auctionCreated = mapper.Map<AuctionCreated>(auctionDto);
        await publishEndpoint.Publish(auctionCreated);

        var result = await context.SaveChangesAsync() > 0;

        return result ?
            CreatedAtAction(nameof(GetAuctionById), new { id = auction.Id }, auctionDto) :
            BadRequest("Could not create auction");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto updateAuctionDto)
    {
        if (await context.Auctions.Include(x => x.Item).FirstOrDefaultAsync(a => a.Id == id)
            is not { } auction)
        {
            return NotFound("Auction not found");
        }
        
        // TODO: check seller == username
        auction.Item.Make = updateAuctionDto.Make ?? auction.Item.Make;
        auction.Item.Model = updateAuctionDto.Model ?? auction.Item.Model;
        auction.Item.Color = updateAuctionDto.Color ?? auction.Item.Color;
        auction.Item.Mileage = updateAuctionDto.Mileage ?? auction.Item.Mileage;
        auction.Item.Year = updateAuctionDto.Year ?? auction.Item.Year;
        
        var auctionDto = mapper.Map<AuctionDto>(auction);
        var auctionUpdated = mapper.Map<AuctionUpdated>(auctionDto);
        await publishEndpoint.Publish(auctionUpdated);

        var result = await context.SaveChangesAsync() > 0;
        
        return result ?
            Ok() :
            BadRequest("Could not update auction");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuction(Guid id)
    {
        if (await context.Auctions.FindAsync(id) is not { } auction)
        {
            return NotFound();
        }
        
        // TODO: check seller == username
        var auctionDeleted = mapper.Map<AuctionDeleted>(id);
        await publishEndpoint.Publish(auctionDeleted);
        context.Auctions.Remove(auction);

        var result = await context.SaveChangesAsync() > 0;
        
        return result ?
            Ok() :
            BadRequest("Could not delete auction");
    }
}