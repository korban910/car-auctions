using AuctionService.Context.Interfaces;
using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionService.Controllers;

[ApiController]
[Route("api/auctions")]
public class AuctionsController(
    IAuctionRepository repository, 
    IMapper mapper,
    IPublishEndpoint publishEndpoint) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions(string? date)
    {
        return await repository.GetAuctionsAsync(date);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
    {
        if (await repository.GetAuctionByIdAsync(id) is not { } auction)
        {
            return NotFound();
        }
        
        return mapper.Map<AuctionDto>(auction);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto createAuctionDto)
    {
        var auction = mapper.Map<Auction>(createAuctionDto);
        
        auction.Seller = User.Identity?.Name ?? "Unknown";
        
        await repository.AddAuctionAsync(auction);
        
        var auctionDto= mapper.Map<AuctionDto>(auction);
        var auctionCreated = mapper.Map<AuctionCreated>(auctionDto);
        await publishEndpoint.Publish(auctionCreated);

        var result = await repository.SaveChangesAsync(CancellationToken.None);

        return result ?
            CreatedAtAction(nameof(GetAuctionById), new { id = auction.Id }, auctionDto) :
            BadRequest("Could not create auction");
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto updateAuctionDto)
    {
        if (await repository.GetAuctionEntityById(id)
            is not { } auction)
        {
            return NotFound("Auction not found");
        }

        if (auction.Seller != User.Identity?.Name)
        {
            return Forbid();
        }
        
        auction.Item.Make = updateAuctionDto.Make ?? auction.Item.Make;
        auction.Item.Model = updateAuctionDto.Model ?? auction.Item.Model;
        auction.Item.Color = updateAuctionDto.Color ?? auction.Item.Color;
        auction.Item.Mileage = updateAuctionDto.Mileage ?? auction.Item.Mileage;
        auction.Item.Year = updateAuctionDto.Year ?? auction.Item.Year;
        
        var auctionDto = mapper.Map<AuctionDto>(auction);
        var auctionUpdated = mapper.Map<AuctionUpdated>(auctionDto);
        await publishEndpoint.Publish(auctionUpdated);

        var result = await repository.SaveChangesAsync(CancellationToken.None);
        
        return result ?
            Ok() :
            BadRequest("Could not update auction");
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuction(Guid id)
    {
        if (await repository.GetAuctionEntityById(id) is not { } auction)
        {
            return NotFound();
        }
        
        if (auction.Seller != User.Identity?.Name)
        {
            return Forbid();
        }
        
        var auctionDeleted = mapper.Map<AuctionDeleted>(id);
        await publishEndpoint.Publish(auctionDeleted);
        repository.RemoveAuction(auction);

        var result = await repository.SaveChangesAsync(CancellationToken.None);
        
        return result ?
            Ok() :
            BadRequest("Could not delete auction");
    }
}