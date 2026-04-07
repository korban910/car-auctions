using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using Contracts;

namespace AuctionService.Mapping;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Auction, AuctionDto>()
            .IncludeMembers(a => a.Item);

        CreateMap<AuctionService.Entities.Item, ItemDto>();

        CreateMap<AuctionService.Entities.Item, AuctionDto>();
        
        CreateMap<CreateAuctionDto, Auction>()
            .ForMember(d => d.Item, 
                o => o.MapFrom(s => s));

        CreateMap<CreateAuctionDto, AuctionService.Entities.Item>();

        CreateMap<ItemDto, Contracts.Item>();

        CreateMap<AuctionDto, AuctionCreated>()
            .ForMember(d => d.Item,
                o => o.MapFrom(s => s.Item));
    }
}