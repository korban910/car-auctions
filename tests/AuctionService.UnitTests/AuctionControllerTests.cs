using AuctionService.Context.Interfaces;
using AuctionService.Controllers;
using AuctionService.DTOs;
using AuctionService.Entities;
using AuctionService.Mapping;
using AuctionService.UnitTests.Utils;
using AutoFixture;
using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        _auctionsController = new AuctionsController(_auctionRepository.Object, _mapper, _publishEndpoint.Object)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext
                {
                    User = Helpers.GetClaimsPrincipal()
                }
            }
        };
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

    [Fact]
    public async Task CreateAuction_WithValidaCreateAuctionDto_ReturnsCreatedAtAction()
    {
        // arrange
        var auction = _fixture.Create<CreateAuctionDto>();
        _auctionRepository.Setup(repo => repo.AddAuctionAsync(It.IsAny<Auction>()));
        _auctionRepository.Setup(repo => repo.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(true);
        
        // act
        var result = await _auctionsController.CreateAuction(auction);
        var createdResult = result.Result as CreatedAtActionResult;
        
        // assert
        Assert.NotNull(createdResult);
        Assert.Equal("GetAuctionById", createdResult.ActionName);
        Assert.IsType<AuctionDto>(createdResult.Value);
    }
    
    [Fact]
    public async Task CreateAuction_FailedSave_Returns400BadRequest()
    {
        // arrange
        var auction = _fixture.Create<CreateAuctionDto>();
        _auctionRepository.Setup(repo => repo.AddAuctionAsync(It.IsAny<Auction>()));
        _auctionRepository.Setup(repo => repo.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(false);
        
        // act
        var result = await _auctionsController.CreateAuction(auction);
        
        // assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task UpdateAuction_WithUpdateAuctionDto_ReturnsOkResponse()
    {
       // arrange
       var auction = _fixture.Build<Auction>().Without(x => x.Item).Create();
       auction.Seller = "test";
       auction.Item = _fixture.Build<Item>().Without(x => x.Auction).Create();
       var updatedDto = _fixture.Create<UpdateAuctionDto>();
       _auctionRepository.Setup(repo => repo.GetAuctionEntityById(It.IsAny<Guid>()))
           .ReturnsAsync(auction);
       _auctionRepository.Setup(repo => repo.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(true);
       
       // act
       var result = await _auctionsController.UpdateAuction(auction.Id, updatedDto);
       
       // assert
       Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task UpdateAuction_WithInvalidUser_Returns403Forbid()
    {
        // arrange
        var auction = _fixture.Build<Auction>().Without(x => x.Item).Create();
        auction.Item = _fixture.Build<Item>().Without(x => x.Auction).Create();
        var updatedDto = _fixture.Create<UpdateAuctionDto>();
        _auctionRepository.Setup(repo => repo.GetAuctionEntityById(It.IsAny<Guid>()))
            .ReturnsAsync(auction);
       
        // act
        var result = await _auctionsController.UpdateAuction(auction.Id, updatedDto);
       
        // assert
        Assert.IsType<ForbidResult>(result);
    }

    [Fact]
    public async Task UpdateAuction_WithInvalidGuid_ReturnsNotFound()
    {
        // arrange
        var auction = _fixture.Build<Auction>().Without(x => x.Item).Create();
        auction.Item = _fixture.Build<Item>().Without(x => x.Auction).Create();
        var updatedDto = _fixture.Create<UpdateAuctionDto>();
        _auctionRepository.Setup(repo => repo.GetAuctionEntityById(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);
       
        // act
        var result = await _auctionsController.UpdateAuction(auction.Id, updatedDto);
       
        // assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task DeleteAuction_WithValidUser_ReturnsOkResponse()
    {
        // arrange
        var auction = _fixture.Build<Auction>().Without(x => x.Item).Create();
        auction.Seller = "test";
        _auctionRepository.Setup(repo => repo.GetAuctionEntityById(It.IsAny<Guid>()))
            .ReturnsAsync(auction);
        _auctionRepository.Setup(repo => repo.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(true);
        
        // act
        var result = await _auctionsController.DeleteAuction(auction.Id);
        
        // assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteAuction_WithInvalidGuid_Returns404Response()
    {
        // arrange
        var auction = _fixture.Build<Auction>().Without(x => x.Item).Create();
        _auctionRepository.Setup(repo => repo.GetAuctionEntityById(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);
        
        // act
        var result = await _auctionsController.DeleteAuction(auction.Id);
        
        // assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteAuction_WithInvalidUser_Returns403Response()
    {
        // arrange
        var auction = _fixture.Build<Auction>().Without(x => x.Item).Create();
        _auctionRepository.Setup(repo => repo.GetAuctionEntityById(It.IsAny<Guid>()))
            .ReturnsAsync(auction);
        
        // act
        var result = await _auctionsController.DeleteAuction(auction.Id);
        
        // assert
        Assert.IsType<ForbidResult>(result);
    }
}