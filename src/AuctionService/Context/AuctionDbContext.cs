using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Context;

public class AuctionDbContext(DbContextOptions<AuctionDbContext> options) : DbContext (options)
{
    public DbSet<Auction> Auctions { get; set; }
    public DbSet<Item> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}