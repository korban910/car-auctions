using AutoMapper;
using Contracts;
using SearchService.Entities;

namespace SearchService.Mapping;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Contracts.Item, SearchService.Entities.Item>();
        
        CreateMap<AuctionCreated, Auction>();
    }
}