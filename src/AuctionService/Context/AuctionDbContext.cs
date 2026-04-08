using AuctionService.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Context;

public class AuctionDbContext(DbContextOptions<AuctionDbContext> options) : DbContext (options)
{
    public DbSet<Auction> Auctions { get; set; }
    public DbSet<Item> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
        
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}