using AutoMapper;
using BiddingService.DTOs;
using BiddingService.Entities;
using Contracts;

namespace BiddingService.Mapping;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Auction, AuctionDto>();
        
        CreateMap<Bid, BidDto>()
            .ForMember(d => d.BidStatus, 
                opt => opt.MapFrom(s => s.BidStatus.Name))
            .ForMember(d => d.BidStatusHtml,
                opt => opt.MapFrom(s => s.BidStatus.Html));

        CreateMap<Bid, BidPlaced>()
            .ForMember(d => d.AuctionId,
                opt => opt.MapFrom(s => Guid.Parse(s.AuctionId)))
            .ForMember(d => d.BidStatus,
                opt => opt.MapFrom(s => s.BidStatus.Name));
    }
}