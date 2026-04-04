using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Entities;

namespace SearchService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Auction>>> SearchItems(
        string? searchTerm,
        int pageNumber = 1,
        int pageSize = 4)
    {
        // DB.Find<Auction> <-- without pagination
        var query = DB.PagedSearch<Auction>();

        query.Sort(x => x.Ascending(a => a.Item.Make));

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query.Match(Search.Full, searchTerm).SortByTextScore();
        }
        
        query.PageSize(pageNumber);
        query.PageSize(pageSize);

        var result = await query.ExecuteAsync();
        
        return Ok(new
        {
            results = result.Results,
            pageCount = result.PageCount,
            totalCount = result.TotalCount
        });
    }
}