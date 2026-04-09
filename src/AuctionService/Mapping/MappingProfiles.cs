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

        CreateMap<AuctionDto, AuctionUpdated>()
            .ForMember(d => d.Make,
                o => o.MapFrom(s => s.Item.Make))
            .ForMember(d => d.Model,
                o => o.MapFrom(s => s.Item.Model))
            .ForMember(d => d.Year,
                o => o.MapFrom(s => s.Item.Year))
            .ForMember(d => d.Color,
                o => o.MapFrom(s => s.Item.Color))
            .ForMember(d => d.Mileage,
                o => o.MapFrom(s => s.Item.Mileage));

        CreateMap<Guid, AuctionDeleted>()
            .ForMember(d => d.Id, 
                opt => opt.MapFrom(src => src));
    }
}