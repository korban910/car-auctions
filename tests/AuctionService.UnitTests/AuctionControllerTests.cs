using AuctionService.Context.Interfaces;
using AuctionService.Controllers;
using AuctionService.DTOs;
using AuctionService.Entities;
using AuctionService.Mapping;
using AutoFixture;
using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AuctionService.UnitTests;

public class AuctionControllerTests
{
    private readonly Mock<IAuctionRepository> _auctionRepository;
    private readonly Mock<IPublishEndpoint> _publishEndpoint;
    private readonly Fixture _fixture;
    private readonly AuctionsController _auctionsController;
    private readonly IMapper _mapper;
        
    public AuctionControllerTests()
    {
        _fixture = new Fixture();
        _auctionRepository = new Mock<IAuctionRepository>();
        _publishEndpoint = new Mock<IPublishEndpoint>();
        
        var mockMapper = new MapperConfiguration(mc =>
        {
            mc.AddMaps(typeof(MappingProfiles).Assembly);
        }).CreateMapper().ConfigurationProvider;
        
        _mapper = new Mapper(mockMapper);
        _auctionsController = new AuctionsController(_auctionRepository.Object, _mapper, _publishEndpoint.Object);
    }

    [Fact]
    public async Task GetAllAuctions_WithNoParams_Returns10Auctions()
    {
        // arrange
        var auctions = _fixture.CreateMany<AuctionDto>(10).ToList();
        _auctionRepository.Setup(repo => repo.GetAuctionsAsync(null))
            .ReturnsAsync(auctions);
        
        // act
        var result = await _auctionsController.GetAllAuctions(null);
        
        // assert
        Assert.Equal(10, result.Value?.Count);
        Assert.IsType<ActionResult<List<AuctionDto>>>(result);
    }
    
    [Fact]
    public async Task GetAuctionById_WithValidGuid_ReturnsAuction()
    {
        // arrange
        var auction = _fixture.Create<AuctionDto>();
        _auctionRepository.Setup(repo => repo.GetAuctionByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(auction);
        
        // act
        var result = await _auctionsController.GetAuctionById(auction.Id);
        
        // assert
        Assert.Equal(auction.Item.Make, result.Value?.Item.Make);
        Assert.Equal(auction.Id, result.Value?.Id);
        Assert.IsType<ActionResult<AuctionDto>>(result);
    }

    [Fact]
    public async Task GetAuctionById_WithInvalidGuid_ReturnsNotFound()
    {
        // arrange
        _auctionRepository.Setup(repo => repo.GetAuctionByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);
        
        // act
        var result = await _auctionsController.GetAuctionById(Guid.NewGuid());
        
        // assert
        Assert.IsType<NotFoundResult>(result.Result);
        Assert.Null(result.Value);
    }
}