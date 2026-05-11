using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Common.Helpers;
using SearchService.Entities;

namespace SearchService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Auction>>> SearchItems([FromQuery] SearchParams searchParams)
    {
        // DB.Find<Auction> <-- without pagination
        var query = DB.PagedSearch<Auction, Auction>();

        if (!string.IsNullOrWhiteSpace(searchParams.SearchTerm))
            query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();

        query = searchParams.OrderBy switch
        {
            "make" => query
                .Sort(x => x.Ascending(a => a.Item.Make))
                .Sort(x => x.Ascending(a => a.Item.Model)),
            "new" => query.Sort(x => x.Descending(a => a.CreatedAt)),
            _ => query.Sort(x => x.Ascending(a => a.AuctionEnd))
        };

        query = searchParams.FilterBy switch
        {
            "finished" => query.Match(x => x.AuctionEnd < DateTimeOffset.UtcNow),
            "endingSoon" => query.Match(x => x.AuctionEnd < DateTimeOffset.UtcNow.AddHours(6)
                                             && x.AuctionEnd > DateTimeOffset.UtcNow),
            _ => query.Match(x => x.AuctionEnd > DateTimeOffset.UtcNow)
        };

        if (!string.IsNullOrWhiteSpace(searchParams.Seller)) query.Match(x => x.Seller == searchParams.Seller);

        if (!string.IsNullOrWhiteSpace(searchParams.Winner)) query.Match(x => x.Winner == searchParams.Winner);

        query.PageNumber(searchParams.PageNumber);
        query.PageSize(searchParams.PageSize);

        var result = await query.ExecuteAsync();

        return Ok(new
        {
            results = result.Results,
            pageCount = result.PageCount,
            totalCount = result.TotalCount
        });
    }
}
