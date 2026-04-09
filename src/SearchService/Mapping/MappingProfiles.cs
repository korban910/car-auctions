using AutoMapper;
using Contracts;
using SearchService.Entities;

namespace SearchService.Mapping;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Contracts.Item, SearchService.Entities.Item>();
        CreateMap<AuctionUpdated, SearchService.Entities.Item>();
        
        CreateMap<AuctionCreated, Auction>();
        
        CreateMap<AuctionUpdated, Auction>()
            .ForMember(d => d.ID, 
                opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Item, 
                o => o.MapFrom(s => s));
        
        CreateMap<AuctionDeleted, Auction>()
            .ForMember(d => d.ID, 
                opt => opt.MapFrom(src => src.Id));
    }
}